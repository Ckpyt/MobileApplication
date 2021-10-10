
namespace MobileApplication
{
    /// <summary>
    /// struct for holding data for restoring invoices from database (tblInvoices)
    /// used the same structure as in the database, excepted UserName - it gets from tblUsers
    /// </summary>
    public struct Invoice
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
    /// struct for holding data for restoring data from the database
    /// used the same structure as in the tblSubInvoice
    /// </summary>
    public struct SubInvoice
    {
        public int id;
        public int invoiceId;
        public string device;
        public string description;
        public int price;
        public int count;
    }
}
