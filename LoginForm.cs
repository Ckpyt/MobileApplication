﻿using System;
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
    /// A class for logining into apllication
    /// The application should not run without logging in.
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary> Is someone loggined? </summary>
        bool isSomeoneLogged = false;

        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show login error
        /// </summary>
        void ShowError()
        {
            MessageBox.Show("Sorry, invalid username or password!", "Login error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Login button pressed
        /// Check login name and password
        /// </summary>
        private void Button1_Click(object sender, EventArgs e)
        {
            var usersList = SQLWorker.GetInstance().ReadUser(nameBox.Text);
            if (usersList.Count == 0)
            {
                ShowError();
                return;
            }

            foreach(var usr in usersList)
            {
               if(usr.Value.ComparePasswords(passwordBox.Text))
                {
                    MainForm.currentUser = usr.Value;
                    isSomeoneLogged = true;
                    Close();
                    return;
                }
            }
                ShowError();
        }

        /// <summary>
        /// if no one logged, application should be closed.
        /// If someone logged, only this form should be closed
        /// </summary>
        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isSomeoneLogged)
                Application.Exit();
        }
    }
}
