using SHA3.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace MobileApplication
{
    /// <summary>
    /// main class. Hold all the tabs and main menu
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary> current user. Choosed in LoginForm </summary>
        public static User currentUser = null;

        public MainForm()
        {
            InitializeComponent();
            SQLWorker.GetInstance();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
            /// TabPage class cannot be designed by designManager. For this reason, in the debug mode, 
            /// ..Page classes inherited from Form page and can be launched by pressing buttons
#if DEBSYMB
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();

            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(-1, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "tmpMakeInvoice";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(94, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "tmpKeywords";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(177, 23);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "tmpCustoms";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(259, 23);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "tmpLogs";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);

            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);

#else
            if (currentUser != null)
            {

                FillTabs();
                this.Controls.Add(this.AllTabs);
            }
#endif


        }

#if DEBSYMB
#else
        /// <summary>
        /// reading user rights and set them
        /// </summary>
        void FillTabs()
        {
            string userRights = currentUser.GetStringRights();
            ObjectsPage obj = null;
            MakeInvoicePage invoice = null;
            UsersPage usersPage = null;
            LogsPage logs = null;

            if (userRights.Contains('D'))
                obj = new ObjectsPage();
            if (userRights.Contains('I'))
                invoice = new MakeInvoicePage();
            if (userRights.Contains('U'))
                usersPage = new UsersPage();
            if (userRights.Contains('L'))
                logs = new LogsPage();

            if (invoice != null) AllTabs.TabPages.Add(invoice);
            if (obj != null) AllTabs.TabPages.Add(obj);
            if (usersPage != null) AllTabs.TabPages.Add(usersPage);
            if (logs != null) AllTabs.TabPages.Add(logs);
        }
#endif

        /// <summary>
        /// Making SHA3-256 hash for password.
        /// </summary>
        /// <param name="input"> password in string </param>
        /// <returns> hash SHA3-256 </returns>
        public static byte[] ComputeHash(string input)
        {
            byte[] byteArray = Encoding.UTF32.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            
            var hash = Sha3.Sha3256().ComputeHash(stream);

            stream = new MemoryStream(hash);
            StreamReader rdr = new StreamReader(stream);
            string tst = rdr.ReadToEnd();
            return hash;
        }

#if DEBSYMB
        //tmp button for testing MakeInvoicePage. Later, should be deleted.
        private void Button1_Click(object sender, EventArgs e)
        {
            MakeInvoicePage mkf = new MakeInvoicePage();
            mkf.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ObjectsPage page = new ObjectsPage();
            page.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            UsersPage page = new UsersPage();
            page.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            LogsPage page = new LogsPage();
            page.Show();
        }
#endif

        /// <summary>
        /// Closing application
        /// </summary>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// switch on MakeInvoice tab and clear it
        /// </summary>
        private void NewInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if DEBSYMB
#else
            MakeInvoicePage inv = AllTabs.TabPages[0] as MakeInvoicePage;
            AllTabs.SelectedIndex = 0;
            AllTabs.SelectedTab = AllTabs.TabPages[0];
            inv.Button1_Click(sender, e);
#endif
        }

        /// <summary>
        /// Show About dialog
        /// </summary>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        /// <summary>
        /// show settings dialog
        /// </summary>
        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings stg = new Settings();
            stg.ShowDialog();
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm lgf = new LoginForm();
            lgf.ShowDialog();
#if DEBSYMB
#else
            AllTabs.TabPages.Clear();
            FillTabs();
#endif
        }
    }
}
