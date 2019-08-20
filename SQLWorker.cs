using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobileApplication
{
    /// <summary>
    /// Class for working with database. 
    /// Should hold all the query to database, check the structure and fix it.
    /// </summary>
    class SQLWorker
    {
        string connectingString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Cpp\\MobileApplication\\MobileInvoce.mdf;Integrated Security=True;Connect Timeout=30";
        string dataBasePath = "";
        /// <summary>
        /// store only one object
        /// </summary>
        static SQLWorker worker = new SQLWorker();

        /// <summary>
        /// private constructor allows to create only managing count of objects
        /// part of singletone
        /// </summary>
        private SQLWorker()
        {
            Logger.GetInstance().SaveLog("SQLWorker const enter");
            if(CheckDatabase())
                CheckAndCreateTables();
            Logger.GetInstance().SaveLog("SQLWorker const exit");
        }

        /// <summary>
        /// check where is database file located. It could be in the same directory or higher
        /// </summary>
        bool CheckDatabase()
        {
            bool result = false;
            Logger.GetInstance().SaveLog("SQLWorker CheckDatabase() enter");
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            try
            {
                dataBasePath = configuration.AppSettings.Settings["dataBaseFile"].Value;
                if (dataBasePath[1] != ':')
                {
                    dataBasePath = Directory.GetCurrentDirectory() + "\\" + dataBasePath;
                    configuration.AppSettings.Settings["dataBaseFile"].Value = dataBasePath;
                    configuration.Save(ConfigurationSaveMode.Full, true);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
            catch (Exception ex)
            {
                configuration.AppSettings.Settings.Add("dataBaseFile", "MobileInvoice.mdf");
                configuration.AppSettings.Settings.Add("invoiceFile", "Invoice tempete.docx");
                configuration.AppSettings.Settings.Add("outputDirectory", "");
                configuration.Save(ConfigurationSaveMode.Full, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
            string path = dataBasePath;

            if (File.Exists(path)){
                connectingString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + path +
               ";Integrated Security=True;Connect Timeout=30";
                Logger.GetInstance().SaveLog("SQLWorker CheckDatabase() exit 1");
                return true;
            }
            else
            {
                Settings stg = new Settings();
                var dialRes = stg.ShowDialog();
                if (dialRes != DialogResult.OK)
                {
                    Logger.GetInstance().SaveLog("SQLWorker CheckDatabase() exit noOk");
                    return false;
                }

                configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                path = configuration.AppSettings.Settings["dataBaseFile"].Value;

                Logger.GetInstance().SaveLog("SQLWorker CheckDatabase() createDatabase");

                string dbname = Path.GetFileNameWithoutExtension(path);

                CreateDatabase(dbname, path);
                result |=CheckDatabase();
            }
            Logger.GetInstance().SaveLog("SQLWorker CheckDatabase() exit 2");
            return result;
        }

        public int GetMaxId(string tblName)
        {
            Logger.GetInstance().SaveLog("SQLWorker GetMaxId() enter");
            SqlConnection conn = new SqlConnection(connectingString);
            SqlCommand comm = new SqlCommand("select max(Id) from " + tblName, conn);
            conn.Open();
            SqlDataReader reader = null;
            int maxId = 0;
            try
            {
                reader = comm.ExecuteReader();
                if (reader.HasRows && reader.Read())
                    maxId = Convert.ToInt32(reader[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Table was not found\n" + ex.Message, "SqlException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Logger.GetInstance().SaveLog("SQLWorker GetMaxId() exit");
            return maxId;
        }

        public static bool DetachDatabase(string dbName)
        {
            try
            {
                string connectionString = String.Format(@"Data Source=(LocalDB)\v11.0;Initial Catalog=master;Integrated Security=True");
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = String.Format("exec sp_detach_db '{0}'", dbName);
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        public static void CreateDatabase(string dbName, string dbFileName)
        {
            if (dbFileName[1] != ':')
                dbFileName = Directory.GetCurrentDirectory() + "\\" + dbFileName;

            String str;
            SqlConnection myConn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Integrated security=SSPI");

            string logDbFile =  Directory.GetParent(dbFileName) + "\\" + dbName + ".ldf";

            str = "CREATE DATABASE " + dbName + " ON PRIMARY " +
                "(NAME =  "+ dbName + "_data, " +
                "FILENAME =  '" + dbFileName + "', " +
                "SIZE = 5MB, MAXSIZE = 20MB, FILEGROWTH = 10%) " +
                "LOG ON (NAME =" + dbName + "_log, " +
                "FILENAME = '"+ logDbFile + "', " +
                "SIZE = 5MB, " +
                "MAXSIZE = 10MB, " +
                "FILEGROWTH = 10%)";

            SqlCommand alter = new SqlCommand("ALTER DATABASE tempdb MODIFY FILE ( NAME = N'DDAS_log', FILENAME = N'DDAS_log.ldf')", myConn);
            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                alter.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            { myConn.Close(); }

            try { 
                
                myConn.Open();
                myCommand.ExecuteNonQuery();
                MessageBox.Show("DataBase is Created Successfully", "Sql created", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


            /// <summary>
            /// Check has the database a table. If not, it will be created
            /// </summary>
            /// <param name="checkingCommand"> command for checking table. it could be ""select max(Id) from Table"" </param>
            /// <param name="creatingCommand"> command for create a table. It could be copyed from Table Definition </param>
            /// /// <returns>retirns true if table was created</returns>
            bool CheckAndCreateTable(string checkingCommand, string creatingCommand)
        {
            Logger.GetInstance().SaveLog("SQLWorker CheckAndCreateTable() enter " + checkingCommand + " " 
                + creatingCommand + " " + connectingString);

            if (connectingString == null || connectingString == "")
                CheckDatabase();

            SqlConnection conn = new SqlConnection(connectingString);
            SqlCommand comm = new SqlCommand(checkingCommand, conn);
            conn.Open();

            SqlDataReader reader = null;
            try
            {
                reader = comm.ExecuteReader();
                if (!reader.HasRows)
                    reader = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Table was not found and it will be created:\n" + ex.Message, "SqlException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (reader == null)
            {
                conn.Close();
                conn.Open();

                comm = new SqlCommand(creatingCommand, conn);

                comm.ExecuteNonQuery();
                Logger.GetInstance().SaveLog("SQLWorker CheckAndCreateTable() exit1");
                return true;
            }
            Logger.GetInstance().SaveLog("SQLWorker CheckAndCreateTable() exit2");
            return false;
        }

        /// <summary>
        /// Check does database has tables. If not, them will be created
        /// </summary>
        void CheckAndCreateTables()
        {
            bool check = CheckAndCreateTable("select max(Id) from tblUsers", 
                "CREATE TABLE [dbo].[tblUsers] ([Id] INT NOT NULL, [Name] varchar(max) NOT NULL, [Phone] varchar(20) NOT NULL, [Rights] INT NULL, [PassHash] VARBINARY (32) NULL, PRIMARY KEY CLUSTERED ([Id] ASC) );");
            if (check)
            {
                User user = new User();
                user.name = "Adm";
                user.id = 1;
                user.SetPassword("adm");
                user.SetStringRights("IDPUL");
                user.phone = "1111";
                SqlComm(user.InsertNewUser);
            }

            if(CheckAndCreateTable("select max(Id) from tblPhoneModels", "CREATE TABLE [dbo].[tblPhoneModels] ([Id] INT NOT NULL, [Name] varchar(max) NOT NULL, [ParentId] INT NULL, PRIMARY KEY CLUSTERED ([Id] ASC) );")){
                SqlComm(() =>
                {
                    return new SqlCommand("insert into tblPhoneModels values(1,'Samsung', 0);");
                });
                SqlComm(() =>
                {
                    return new SqlCommand("insert into tblPhoneModels values(2,'Samsung 6s', 1);");
                });
            }
            CheckAndCreateTable("select max(Id) from tblFunctions", "CREATE TABLE [dbo].[tblFunctions] ([Id] INT NOT NULL, [Name] varchar(max) NOT NULL, [Price] INT NULL, PRIMARY KEY CLUSTERED ([Id] ASC) );");
            CheckAndCreateTable("select max(Id) from tblOperations", "CREATE TABLE [dbo].[tblOperations] ([Id] INT NOT NULL, [DeviceId] int NOT NULL, [FunctionId] INT NOT NULL, [Price] INT NULL, PRIMARY KEY CLUSTERED ([Id] ASC) );");
            CheckAndCreateTable("select max(Id) from tblInvoices", "CREATE TABLE [dbo].[tblInvoices] ([Id] INT NOT NULL PRIMARY KEY, [Date] Datetime, [CustName] varchar(max), [UserId] int NOT NULL, [TotalPrice] int, [Devices] varchar(max)); ");
            CheckAndCreateTable("select max(Id) from tblSubInvoices", "CREATE TABLE [dbo].[tblSubInvoices] ([Id] INT NOT NULL PRIMARY KEY, [InvoiceID] int not null, [Device] varchar(max), [Description] varchar(max), [Price] int, [Count] int); ");


        }

        /// <summary>
        /// Part of singletone
        /// </summary>
        /// <returns>retirns only one object</returns>
        public static SQLWorker GetInstance()
        {
            return worker;
        }

        /// <summary>
        /// Executing current sql command with no result
        /// </summary>
        /// <param name="writeCommand"> text for single sql command </param>
        public void SqlComm(string comm)
        {
            SqlComm(new SqlCommand(comm));
        }

        /// <summary>
        /// Executing current sql command with no result
        /// </summary>
        /// <param name="writeCommand"> single sql command </param>
        public void SqlComm(SqlCommand comm)
        {
            SqlConnection conn = new SqlConnection(connectingString);
            comm.Connection = conn;
            conn.Open();
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SqlException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Executing current sql command with no result
        /// </summary>
        /// <param name="writeCommand"> function for making single sql command </param>
        public void SqlComm(Func<SqlCommand> writeCommand)
        {
            SqlCommand comm = writeCommand();
            SqlComm(comm);
        }

        /// <summary>
        /// Get all the users from database
        /// </summary>
        /// <returns> list of all the users </returns>
        public SortedList<int, User> ReadUsers()
        {
            SqlCommand comm = new SqlCommand("select * from tblUsers");
            return GetUsersFromDatabase(comm);
        }

        /// <summary>
        /// Read users from database with existing sql command
        /// </summary>
        /// <param name="comm"> presetted sql command </param>
        /// <returns> list of users </returns>
        SortedList<int, User> GetUsersFromDatabase(SqlCommand comm)
        {
            SqlConnection conn = new SqlConnection(connectingString);
            comm.Connection = conn; 
            SortedList<int, User> answ = new SortedList<int, User>();
            SqlDataReader result = null;
            conn.Open();

            try
            {
                result = comm.ExecuteReader();
                while (result.HasRows && result.Read())
                {
                    User cust = new User();
                    cust.ReadSqlResult(result);
                    answ.Add(cust.id, cust);
                    //Description.Text = descr;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SqlException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return answ;
        }

        /// <summary>
        /// Read users with a name
        /// </summary>
        /// <param name="name">user's name</param>
        /// <returns>collection of all the users with the same name</returns>
        public SortedList<int, User> ReadUser(string name)
        {
            SqlCommand comm = new SqlCommand("select * from tblUsers where(Name='" + name + "')");
            return GetUsersFromDatabase(comm);
        }

        public List<T> ReadTable<T>(string command, Func<SqlDataReader, T, T> FillReaded )
        {
            SqlConnection conn = new SqlConnection(connectingString);
            SqlCommand comm = new SqlCommand(command, conn);
            List<T> answ = new List<T>();
            SqlDataReader result = null;
            conn.Open();

            try
            {
                result = comm.ExecuteReader();
                while (result.HasRows && result.Read())
                {
                    T readed = (T)Activator.CreateInstance(typeof(T));

                    readed = FillReaded(result, readed);
                    answ.Add(readed);
                    //Description.Text = descr;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SqlException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return answ;
        }

        public List<PhoneModel> ReadPhones()
        {
            return ReadTable<PhoneModel>("select * from tblPhoneModels", (result, phmd) =>
            {
                phmd.id = Convert.ToInt32(result[0]);
                phmd.name = Convert.ToString(result[1]);
                phmd.parentId = Convert.ToInt32(result[2]);
                return phmd;
            });
        }

        public List<Operation> ReadOperations()
        {
            return ReadTable<Operation>("select * from tblOperations", (result, phmd) =>
            {
                phmd.id = Convert.ToInt32(result[0]);
                phmd.deviceID = Convert.ToInt32(result[1]);
                phmd.functionID = Convert.ToInt32(result[2]);
                phmd.price = Convert.ToInt32(result[3]);
                return phmd;
            });
        }

        public List<Function> ReadFunctions()
        {
            return ReadTable<Function>("select * from tblFunctions", (result, phmd) =>
            {
                phmd.id = Convert.ToInt32(result[0]);
                phmd.name = Convert.ToString(result[1]);
                phmd.price = Convert.ToInt32(result[2]);
                return phmd;
            });
        }

    }
}
