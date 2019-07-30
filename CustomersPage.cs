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
        SortedList<int, Customer> customers;
        bool IsSelected = false;
        int SelectedPos = -1;

        public CustomersPage()
        {
            InitializeComponent();
            customers = SQLWorker.GetInstance().ReadCustomers();
            FillListBox();

        }

        private void FillListBox()
        {
            foreach(var custPair in customers)
            {
                Customer cust = custPair.Value;
                ListViewItem itm = new ListViewItem(cust.Name);
                itm.SubItems.Add(cust.Phone);
                itm.SubItems.Add(cust.GetStringRights());
                listView1.Items.Add(itm);
            }
        }

        private void ListView1_SizeChanged(object sender, EventArgs e)
        {
            listView1.Height = Height - 219;
        }

        Customer ChangeDetails(int id, string rights)
        {
            Customer cust = customers[id];
            cust.Name = NameBox.Text;
            cust.Phone = PhoneBox.Text;
            cust.SetStringRights(rights);
            if(PassBox.Text.Length > 0)
                cust.SetPassword(PassBox.Text);
            return cust;
        }

        /// <summary>
        /// Clean all the boxes after ListBox
        /// </summary>
        void CleanFields()
        {
            NameBox.Text = "";
            PhoneBox.Text = "";
            PassBox.Text = "";

            InvoceCheckBox.Checked = false;
            DeviceCheckBox.Checked = false;
            PriceCheckBox.Checked = false;
            CustomersCheckBox.Checked = false;
            LogsCheckBox.Checked = false;
        }

        /// <summary>
        /// add/edit customer
        /// </summary>
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
                Customer cust = ChangeDetails(SelectedPos, rights);
                listView1.Items[SelectedPos] = itm;
                IsSelected = false;
                SQLWorker.GetInstance().SqlComm(cust.UpdateSqlCommand);
                Add.Text = "Add";
            }
            else
            {
                Customer cust = new Customer();
                cust.ID = customers.Count;
                customers.Add(cust.ID, cust);
                ChangeDetails(cust.ID, rights);
                listView1.Items.Add(itm);
                SQLWorker.GetInstance().SqlComm(cust.InsertNewCustomer);
            }

            CleanFields();
        }

        /// <summary>
        /// select customer for editing
        /// </summary>
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
            else
            {
                if (IsSelected)
                {
                    CleanFields();
                    IsSelected = false;
                    Add.Text = "Add";
                }
            }
        }
    }
}
