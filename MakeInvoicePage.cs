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
    /// The page for making invoices. Keywords should be get from database
    /// Result shpuld be saved to database and invoice.pdf
    /// Later, parent class should be changed to tabPage
    /// </summary>
    public partial class MakeInvoicePage : Form
    {

        bool IsSelected = false;
        int SelectedPos = -1;

        public MakeInvoicePage()
        {
            InitializeComponent();
            QTYBox.Text = "0";
            PriceBox.Text = "0";
        }

        private void Add_Click(object sender, EventArgs e)
        {
            ListViewItem itm = new ListViewItem(DeviceBox.Text);
            var itms = itm.SubItems;
            itm.SubItems.Add(DescriptionBox.Text);
            itm.SubItems.Add(QTYBox.Text);
            itm.SubItems.Add(PriceBox.Text);
            if (QTYBox.Text.Length > 0 && PriceBox.Text.Length > 0)
                itm.SubItems.Add((Int32.Parse(QTYBox.Text) * Int32.Parse(PriceBox.Text)).ToString());
            else
                itm.SubItems.Add("0");
            if (IsSelected)
            {
                Add.Text = "Add";
                listView1.Items[SelectedPos] = itm;
                IsSelected = false;
            }
            else
            {
                listView1.Items.Add(itm);
            }

            DeviceBox.Text = "";
            DescriptionBox.Text = "";
            QTYBox.Text = "0";
            PriceBox.Text = "0";
        }

        private void OnlyDecimalChecker(object sender, EventArgs e)
        {
            OnlyDecimalCheckerStatic(sender, e);
        }

        public static void OnlyDecimalCheckerStatic(object sender, EventArgs e)
        {
            TextBox sd = sender as TextBox;
            string txt = "";
            int pos = sd.SelectionStart;

            foreach (char t in sd.Text)
                if (t >= '0' && t <= '9')
                    txt += t;
                else
                    pos--;

            if (txt.Length > 0)
            {
                int tmp = Int32.Parse(txt);
                sd.Text = tmp.ToString();

                sd.SelectionStart = pos;
                sd.SelectionLength = 0;
            }
            else
            {
                sd.Text = "0";
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lst = sender as ListView;


            if(lst.SelectedItems.Count > 0)
            {
                IsSelected = true;
                SelectedPos = lst.SelectedIndices[0];

                var itm = lst.SelectedItems[0];
                DeviceBox.Text = itm.Text;
                DescriptionBox.Text = itm.SubItems[1].Text;
                QTYBox.Text = itm.SubItems[2].Text;
                PriceBox.Text = itm.SubItems[3].Text;
                Add.Text = "Edit";
            }

        }

        private void MakeInvoicePage_ResizeEnd(object sender, EventArgs e)
        {
            int Height = this.Height - 140;
            listView1.Height = Height;
        }
    }
}
