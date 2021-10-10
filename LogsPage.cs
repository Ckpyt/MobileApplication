using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobileApplication
{
    /// <summary>
    /// class for showing stored invoices in the database
    /// </summary>
    public partial class LogsPage :
#if DEBSYMB
        Form
#else
        TabPage
#endif
    {
        /// <summary> list of selected invoices. Changed when search button pressed </summary>
        List<Invoice> selectedInvoices = null;
        /// <summary> list of selected subInvoices. Changed when selected index in  the list of invoices changed </summary>
        List<SubInvoice> selectedSubInvoices = null;

        public LogsPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// holds the same proportions of views everytime
        /// </summary>
        private void LogsPage_SizeChanged(object sender, EventArgs e)
        {
            int elemHeight = (Height - 60) / 2;
            listView1.Height = elemHeight;
            listView2.Location = new Point( 0, 60 + elemHeight);
            listView2.Height = elemHeight;

            listView1.Width = Width;
        }

        /// <summary>
        /// search button click. Load invoices from database
        /// </summary>
        private void Button1_Click(object sender, EventArgs e)
        {
            if(searchCondBox.SelectedIndex > 1 && searchBox.Text == "")
            {
                MessageBox.Show("Sorry, search box could not be empty", "Search error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            int selected = searchCondBox.SelectedIndex;
            SqlCommand command = new SqlCommand();
            selected = selected > -1 ? selected : 0;
            string comm = "select * from tblInvoices,tblUsers where (UserId = tblUsers.Id and ";
            switch (selected)
            {
                case 0:
                    comm += "Date >=@DateFrom and Date <=@DateTo);";
                    command.Parameters.Add(new SqlParameter("DateFrom", fromDateTimePicker.Value.ToString("yyyy-MM-dd")));
                    command.Parameters.Add(new SqlParameter("DateTo", toDateTimePicker.Value.ToString("yyyy-MM-dd")));
                    break;
                case 1:
                    comm += "CustName=@CustomerName)";
                    command.Parameters.Add(new SqlParameter("CustomerName", searchBox.Text));
                    break;
                case 2:
                    comm += "tblUsers.Name=@UserName)";
                    command.Parameters.Add(new SqlParameter("UserName", searchBox.Text));
                    break;
                case 3:
                    comm += "tblInvoices.Id=@InvoiceId)";
                    command.Parameters.Add(new SqlParameter("InvoiceId", searchBox.Text));
                    break;
            }

            command.CommandText = comm;
            selectedInvoices =  SQLWorker.GetInstance().ReadTable<Invoice>(command, (result, invoice) =>
            {
                invoice.id =        Convert.ToInt32(result[0]);
                invoice.date =      Convert.ToString(result[1]);
                invoice.custName =  Convert.ToString(result[2]);
                invoice.userId =    Convert.ToInt32(result[3]);
                invoice.totalPrice = Convert.ToInt32(result[4]);
                invoice.devices =   Convert.ToString(result[5]);
                invoice.userName =  Convert.ToString(result[7]);
                return invoice;
            });

            listView1.Items.Clear();
            foreach(Invoice inv in selectedInvoices)
            {
                ListViewItem itm = new ListViewItem();
                itm.Text = inv.id.ToString();
                itm.SubItems.Add(inv.date);
                itm.SubItems.Add(inv.userName);
                itm.SubItems.Add(inv.custName);
                itm.SubItems.Add(inv.devices);
                itm.SubItems.Add(((float)inv.totalPrice / 100.0f).ToString());
                itm.Tag = inv;
                listView1.Items.Add(itm);
            }

        }

        /// <summary>
        /// loading subInvoices then invoice is selected
        /// </summary>
        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1) return;

            Invoice inv;
            inv = (Invoice)listView1.SelectedItems[0].Tag;

            string comm = "select * from tblSubInvoices where(InvoiceID=" + inv.id.ToString() + ")";

            selectedSubInvoices = SQLWorker.GetInstance().ReadTable<SubInvoice>(comm, (result, subinvoice) =>
            {
                subinvoice.id = Convert.ToInt32(result[0]);
                subinvoice.device = Convert.ToString(result[2]);
                subinvoice.description = Convert.ToString(result[3]);
                subinvoice.price = Convert.ToInt32(result[4]);
                subinvoice.count = Convert.ToInt32(result[5]);
                return subinvoice;
            });

            listView2.Items.Clear();
            foreach(SubInvoice sbinv in selectedSubInvoices)
            {
                ListViewItem itm = new ListViewItem();
                itm.Text = sbinv.device;
                itm.SubItems.Add(sbinv.description);
                itm.SubItems.Add(sbinv.count.ToString());
                itm.SubItems.Add(((float)sbinv.price / 100.0f).ToString());
                itm.SubItems.Add((sbinv.count * (float)sbinv.price / 100.0f).ToString());
                itm.Tag = sbinv;
                listView2.Items.Add(itm);
            }

        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            switch (searchCondBox.SelectedIndex)
            {
                case 0:
                    searchBox.Text = "";
                    break;
                case 3:
                    MakeInvoicePage.OnlyDecimalCheckerStatic(sender, e);
                    break;
            }
        }

        private void SearchCondBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchBox_TextChanged(searchBox, e);
        }
    }
}
