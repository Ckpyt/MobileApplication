using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplication
{
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
