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
    public partial class CustomersPage : Form
    {
        bool IsSelected = false;
        int SelectedPos = -1;
        public CustomersPage()
        {
            InitializeComponent();
        }

        private void ListView1_SizeChanged(object sender, EventArgs e)
        {
            listView1.Height = Height - 219;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ListViewItem itm = new ListViewItem(NameBox.Text);
            itm.SubItems.Add(PhoneBox.Text);
            string rights = "";
            rights += InvoceCheckBox.Checked ?      "I" :"";
            rights += DeviceCheckBox.Checked ?      "D" : "";
            rights += PriceCheckBox.Checked ?       "P" : "";
            rights += CustomersCheckBox.Checked ?   "C" : "";
            rights += LogsCheckBox.Checked ?        "L" : "";

            itm.SubItems.Add(rights);
            
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

            NameBox.Text = "";
            PhoneBox.Text = "";

            InvoceCheckBox.Checked = false;
            DeviceCheckBox.Checked = false;
            PriceCheckBox.Checked = false;
            CustomersCheckBox.Checked = false;
            LogsCheckBox.Checked = false;
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lst = sender as ListView;
            if (lst.SelectedItems.Count > 0)
            {
                IsSelected = true;
                SelectedPos = lst.SelectedIndices[0];

                var itm = lst.SelectedItems[0];
                NameBox.Text = itm.Text;
                PhoneBox.Text = itm.SubItems[1].Text;
                string rights = itm.SubItems[2].Text;
                foreach(char c in rights)
                {
                    switch (c)
                    {
                        case 'I':
                            InvoceCheckBox.Checked = true;
                            break;
                        case 'D':
                            DeviceCheckBox.Checked = true;
                            break;
                        case 'P':
                            PriceCheckBox.Checked = true;
                            break;
                        case 'C':
                            CustomersCheckBox.Checked = true;
                            break;
                        case 'L':
                            LogsCheckBox.Checked = true;
                            break;
                        case ' ':  break;
                        default:
                            throw new Exception("unexpected char " + c  + " founded");
                    }
                }
                Add.Text = "Edit";
            }
        }
    }
}
