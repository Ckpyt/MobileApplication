using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

        /// <summary>
        /// Part of singletone
        /// </summary>
        /// <returns>retirns only one object</returns>
        public static SQLWorker GetInstance()
        {
            return worker;
        }

        public static string FixString(string inp)
        {
            int pos = inp.Length -1;
            while (inp[pos] == ' ' && pos > 0)
                pos--;
            pos++;
            return inp.Substring(0, pos);
        }

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

            }
        }

        /// <summary>
        /// Get all the users from database
        /// </summary>
        /// <returns></returns>
        public SortedList<int, User> ReadUsers()
        {
            SqlCommand comm = new SqlCommand("select * from tblUsers");
            return GetUsersFromDatabase(comm);
        }

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
            SqlCommand comm = new SqlCommand("select * from tblUsers where(Name=\"" + name + "\")");
            return GetUsersFromDatabase(comm);
        }

    }
}
