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
    /// Page for creating users or changing users' details
    /// </summary>
    public partial class UsersPage :
#if DEBSYMB
        Form
#else
        TabPage
#endif
    {
        /// <summary> Full list of users </summary>
        SortedList<int, User> users;
        /// <summary> Is someone selected in the ListView? </summary>
        bool isSelected = false;
        /// <summary> Position of selected user in the ListView </summary>
        int selectedPos = -1;

        /// <summary>
        /// Construct of the class.
        /// It should get all the users from database and fill the listView
        /// </summary>
        public UsersPage()
        {
            InitializeComponent();
            users = SQLWorker.GetInstance().ReadUsers();
            FillListBox();
            delBtn.Visible = false;

        }

        /// <summary>
        /// filling ListBox with users' details
        /// </summary>
        private void FillListBox()
        {
            foreach(var custPair in users)
            {
                User cust = custPair.Value;
                ListViewItem itm = new ListViewItem(cust.id.ToString());
                itm.SubItems.Add(cust.name);
                itm.SubItems.Add(cust.phone);
                itm.SubItems.Add(cust.GetStringRights());
                listView1.Items.Add(itm);
            }
        }

        /// <summary>
        /// scalable size of ListView
        /// </summary>
        private void ListView1_SizeChanged(object sender, EventArgs e)
        {
            listView1.Height = Height - 219;
        }

        /// <summary>
        /// change details of a user
        /// </summary>
        /// <param name="id"> user's id </param>
        /// <param name="rights">user's rights</param>
        /// <returns> the user with new parametres </returns>
        User ChangeDetails(int id, string rights)
        {
            User cust = users[id];
            cust.name = NameBox.Text;
            cust.phone = PhoneBox.Text;
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
            if (PassBox.TextLength == 0 && !isSelected)
            {
                MessageBox.Show("Sorry, password cannot be empty", "Password Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            User cust = null;

            string rights = "";
            rights += InvoceCheckBox.Checked ?      "I" :"";
            rights += DeviceCheckBox.Checked ?      "D" : "";
            rights += PriceCheckBox.Checked ?       "P" : "";
            rights += CustomersCheckBox.Checked ?   "U" : "";
            rights += LogsCheckBox.Checked ?        "L" : "";


            
            if (isSelected)
            {
                int id = Int32.Parse(listView1.Items[selectedPos].Text);
                cust = ChangeDetails(id, rights);
                SQLWorker.GetInstance().SqlComm(cust.UpdateSqlCommand);
            }
            else
            {
                cust = new User();
                cust.id = users.Count > 0 ? users.Last().Value.id + 1 : 1;
                users.Add(cust.id, cust);
                ChangeDetails(cust.id, rights);

                SQLWorker.GetInstance().SqlComm(cust.InsertNewUser);
            }

            ListViewItem itm = new ListViewItem(cust.id.ToString());
            itm.SubItems.Add(NameBox.Text);
            itm.SubItems.Add(PhoneBox.Text);
            itm.SubItems.Add(rights);

            if (isSelected)
            {
                isSelected = false;
                listView1.Items[selectedPos] = itm;
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
                isSelected = true;
                selectedPos = lst.SelectedIndices[0];

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
                        case 'U':
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
                if (isSelected)
                {
                    CleanFields();
                    isSelected = false;
                    Add.Text = "Add";
                }
            }
        }

        /// <summary>
        /// Delete selected user from listview and database
        /// </summary>
        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (!isSelected)
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
