using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            int selected = searchCondBox.SelectedIndex;
            selected = selected > -1 ? selected : 0;
            string comm = "select*from tblInvoices,tblUsers where (UserId = tblUsers.Id and ";
            switch (selected)
            {
                case 0:
                    comm += "Date >='" 
                        + fromDateTimePicker.Value.ToString("yyyy-MM-dd") + 
                        "' and Date <='" + toDateTimePicker.Value.ToString("yyyy-MM-dd") + "');";
                    break;
                case 1:
                    comm += "CustName='" + searchBox.Text + "')";
                    break;
                case 2:
                    comm += "tblInvoices.Id=" + searchBox.Text + ")";
                    break;
                case 3:
                    comm += "tblUsers.Name='" + searchBox.Text + "')";
                    break;
            }

            selectedInvoices =  SQLWorker.GetInstance().ReadTable<Invoice>(comm, (result, invoice) =>
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
        /// loading subiInvoices
        /// </summary>
        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1) return;

            Invoice inv = listView1.SelectedItems[0].Tag as Invoice;
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
    }
}
