using System;
using System.Collections.Generic;
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
        static SQLWorker worker = new SQLWorker();

        private SQLWorker()
        {
        }

        public static SQLWorker GetInstance()
        {
            return worker;
        }

        public void UpdateCustomer(Customer cust)
        {

        }

        public void AddCustomer(Customer cust)
        {

        }

        public Customer[] ReadCustomers()
        {
            return null;
        }
    }
}
