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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        void ShowError()
        {
            MessageBox.Show("Sorry, your username and password does not founded", "Login error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var usersList = SQLWorker.GetInstance().ReadUser(NameBox.Text);
            if (usersList.Count == 0)
                ShowError();

            bool result = false;
            foreach(var usr in usersList)
            {
               if(usr.Value.ComparePasswords(PasswordBox.Text))
                {
                    MainForm.currentUser = usr.Value;
                    Close();
                }
            }
                ShowError();
        }
    }
}
