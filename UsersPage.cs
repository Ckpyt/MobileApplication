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
    public partial class UsersPage : Form
    {
        SortedList<int, User> users;
        bool IsSelected = false;
        int SelectedPos = -1;

        public UsersPage()
        {
            InitializeComponent();
            users = SQLWorker.GetInstance().ReadUsers();
            FillListBox();
            delBtn.Visible = false;

        }

        private void FillListBox()
        {
            foreach(var custPair in users)
            {
                User cust = custPair.Value;
                ListViewItem itm = new ListViewItem(cust.ID.ToString());
                itm.SubItems.Add(cust.Name);
                itm.SubItems.Add(cust.Phone);
                itm.SubItems.Add(cust.GetStringRights());
                listView1.Items.Add(itm);
            }
        }

        private void ListView1_SizeChanged(object sender, EventArgs e)
        {
            listView1.Height = Height - 219;
        }

        User ChangeDetails(int id, string rights)
        {
            User cust = users[id];
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

            delBtn.Visible = false;
        }

        /// <summary>
        /// add/edit customer
        /// </summary>
        private void Button1_Click(object sender, EventArgs e)
        {
            User cust = null;

            string rights = "";
            rights += InvoceCheckBox.Checked ?      "I" :"";
            rights += DeviceCheckBox.Checked ?      "D" : "";
            rights += PriceCheckBox.Checked ?       "P" : "";
            rights += CustomersCheckBox.Checked ?   "C" : "";
            rights += LogsCheckBox.Checked ?        "L" : "";


            
            if (IsSelected)
            {
                int id = Int32.Parse(listView1.Items[SelectedPos].Text);
                cust = ChangeDetails(id, rights);
                SQLWorker.GetInstance().SqlComm(cust.UpdateSqlCommand);
            }
            else
            {
                cust = new User();
                cust.ID = users.Last().Value.ID + 1;
                users.Add(cust.ID, cust);
                ChangeDetails(cust.ID, rights);

                SQLWorker.GetInstance().SqlComm(cust.InsertNewUser);
            }

            ListViewItem itm = new ListViewItem(cust.ID.ToString());
            itm.SubItems.Add(NameBox.Text);
            itm.SubItems.Add(PhoneBox.Text);
            itm.SubItems.Add(rights);

            if (IsSelected)
            {
                IsSelected = false;
                listView1.Items[SelectedPos] = itm;
                Add.Text = "Add";
            }
            else
            {
                listView1.Items.Add(itm);
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
                NameBox.Text = itm.SubItems[1].Text;
                PhoneBox.Text = itm.SubItems[2].Text;
                string rights = itm.SubItems[3].Text;
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
                delBtn.Visible = true;
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

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (!IsSelected)
                return;

            string id = listView1.SelectedItems[0].Text;

            SQLWorker.GetInstance().SqlComm(() =>
            {
                return new SqlCommand("delete from tblUsers where(ID=" + id + ")");
            });

            listView1.Items.Remove(listView1.SelectedItems[0]);

        }
    }
}
