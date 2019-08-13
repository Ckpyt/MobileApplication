
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

    public partial class MainForm : Form
    {

        public static User currentUser = null;

        public MainForm()
        {
            InitializeComponent();
            SQLWorker.GetInstance();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
#if DEBUG
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();

            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(-1, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "tmpMakeInvoice";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(94, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "tmpKeywords";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(177, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "tmpCustoms";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(259, 12);
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
            string userRights = currentUser.GetStringRights();
            foreach(char c in userRights)
            {
                switch (c)
                {
                    case 'I':
                        AllTabs.TabPages.Add(new MakeInvoicePage());
                        break;
                    case 'D':
                        AllTabs.TabPages.Add(new ObjectsPage());
                        break;
                    case 'P':
                        AllTabs.TabPages.Add(new ObjectsPage());
                        break;
                    case 'U':
                        AllTabs.TabPages.Add(new UsersPage());
                        break;
                    case 'L':
                        AllTabs.TabPages.Add(new LogsPage());
                        break;
                    case ' ': break;
                    default:
                        throw new Exception("unexpected char " + c + " founded");
                }
            }
            this.Controls.Add(this.AllTabs);
#endif


        }

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

#if DEBUG
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
    }
}
