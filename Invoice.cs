using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplication
{
    /// <summary>
    /// class for restoring invoices from database (tblInvoices)
    /// used the same structure as in the database, exceptep UserName - it get from tblUsers
    /// </summary>
    public class Invoice
    {
        public int id;
        public string date;
        public string custName;
        public int userId;
        public int totalPrice;
        public string devices;
        public string userName;
    }

    /// <summary>
    /// class for restoring data from the database
    /// used the same structure as in the tblSubInvoice
    /// </summary>
    public class SubInvoice
    {
        public int id;
        public int invoiceId;
        public string device;
        public string description;
        public int price;
        public int count;
    }
}
