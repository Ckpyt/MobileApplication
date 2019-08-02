﻿using System;
using System.Collections.Generic;
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
            /*ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None).ConnectionStrings.
            ConnectionStrings[0].ConnectionString;*/
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
            CheckDatabase();
            CheckAndCreateTables();
        }

        /// <summary>
        /// check where is database file located. It could be in the same directory or higher
        /// </summary>
        void CheckDatabase()
        {
            string path = Directory.GetCurrentDirectory();
            string fileName = "MobileInvoce.mdf";
            string filePath = path + "\\" + fileName;
            while (!File.Exists(filePath))
            {
                int i = path.Length - 2;
                for (; i > 0; i--)
                {
                    if (path[i] == '\\')
                    {
                        path = path.Substring(0, i + 1);
                        filePath = path + fileName;
                        break;
                    }
                }
                if (i == 0)
                    return;
            }
            connectingString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + filePath +
                ";Integrated Security=True;Connect Timeout=30"; 
        }

        /// <summary>
        /// Check has the database a table. If not, it will be created
        /// </summary>
        /// <param name="checkingCommand"> command for checking table. it could be ""select max(Id) from Table"" </param>
        /// <param name="creatingCommand"> command for create a table. It could be copyed from Table Definition </param>
        /// /// <returns>retirns true if table was created</returns>
        bool CheckAndCreateTable(string checkingCommand, string creatingCommand)
        {
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
                //MessageBox.Show(ex.Message, "SqlException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (reader == null)
            {
                conn.Close();
                conn.Open();

                comm = new SqlCommand(creatingCommand, conn);

                comm.ExecuteNonQuery();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check does database has tables. If not, them will be created
        /// </summary>
        void CheckAndCreateTables()
        {
            if(CheckAndCreateTable("select max(Id) from tblUsers", "CREATE TABLE [dbo].[tblUsers] ([Id] INT NOT NULL, [Name] varchar(max) NOT NULL, [Phone] varchar(20) NOT NULL, [Rights] INT NULL, [PassHash] VARBINARY (32) NULL, PRIMARY KEY CLUSTERED ([Id] ASC) );"))
            {
                User user = new User();
                user.Name = "Adm";
                user.ID = 1;
                user.SetPassword("adm");
                user.SetStringRights("IDPCL");
                user.Phone = "1111";
                SqlComm(user.InsertNewUser);
            }

            CheckAndCreateTable("select max(Id) from tblPhoneModels", "CREATE TABLE [dbo].[tblPhoneModels] ([Id] INT NOT NULL, [Name] varchar(max) NOT NULL, [ParentId] INT NULL, PRIMARY KEY CLUSTERED ([Id] ASC) );");
            CheckAndCreateTable("select max(Id) from tblFunctions", "CREATE TABLE [dbo].[tblFunctions] ([Id] INT NOT NULL, [Name] varchar(max) NOT NULL, [Price] INT NULL, PRIMARY KEY CLUSTERED ([Id] ASC) );");
            CheckAndCreateTable("select max(Id) from tblOperations", "CREATE TABLE [dbo].[tblOperations] ([Id] INT NOT NULL, [DeviceId] int NOT NULL, [FunctionId] INT NOT NULL, [Price] INT NULL, PRIMARY KEY CLUSTERED ([Id] ASC) );");

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
        /// <param name="writeCommand"> single sql command </param>
        public void SqlComm(Func<SqlCommand> writeCommand)
        {
            SqlCommand comm = writeCommand();
            SqlConnection conn = new SqlConnection(connectingString);
            comm.Connection = conn;
            conn.Open();
            try
            {
                comm.ExecuteNonQuery();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "SqlException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    answ.Add(cust.ID, cust);
                    //Description.Text = descr;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
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

    }
}
