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
        /// Get all the customers from database
        /// </summary>
        /// <returns></returns>
        public SortedList<int, Customer> ReadCustomers()
        {
            SqlConnection conn = new SqlConnection(connectingString);
            SqlCommand comm = new SqlCommand("select * from tblCustomers", conn);
            SortedList<int, Customer> answ = new SortedList<int, Customer>();

            

            SqlDataReader result = null;
            conn.Open();

            try
            {
                result = comm.ExecuteReader();
                while (result.HasRows && result.Read())
                {
                    Customer cust = new Customer();
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
        /// Update one customer's details
        /// </summary>
        public void UpdateCustomer(Customer cust)
        {

        }

        /// <summary>
        /// Add a customer to database
        /// </summary>
        public void AddCustomer(Customer cust)
        {

        }
    }
}
