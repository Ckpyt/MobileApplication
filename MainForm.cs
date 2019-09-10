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
using SHA3.Net;


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
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            this.button3.Click += new System.EventHandler(this.Button3_Click);
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

            if (userRights.Contains('I'))
                AllTabs.TabPages.Add(new MakeInvoicePage());
            if (userRights.Contains('D'))
                AllTabs.TabPages.Add(new ObjectsPage());
            if (userRights.Contains('U'))
                AllTabs.TabPages.Add(new UsersPage());
            if (userRights.Contains('L'))
                AllTabs.TabPages.Add( new LogsPage());

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
        //tmp button for testing MakeInvoicePage. 
        private void Button1_Click(object sender, EventArgs e)
        {
            MakeInvoicePage mkf = new MakeInvoicePage();
            mkf.Show();
        }

        //tmp button for testing ObjectsPage. 
        private void Button2_Click(object sender, EventArgs e)
        {
            ObjectsPage page = new ObjectsPage();
            page.Show();
        }

        //tmp button for testing UsersPage. 
        private void Button3_Click(object sender, EventArgs e)
        {
            UsersPage page = new UsersPage();
            page.Show();
        }

        //tmp button for testing LogsPage. 
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
        /// Show settings dialog
        /// </summary>
        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings stg = new Settings();
            stg.ShowDialog();
        }

        /// <summary>
        /// Logout from applocation. 
        /// In release it also readded tabpages into AllTabs
        /// </summary>
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
