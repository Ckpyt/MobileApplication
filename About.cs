using System;
using System.Windows.Forms;

namespace MobileApplication
{
    /// <summary>
    /// class for showing information about developers and our client
    /// </summary>
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        /// <summary>
        /// showing the client information by clicking on the client logo
        /// </summary>
        private void ShowClientPictureBox_Click(object sender, EventArgs e)
        {
            AboutCompany rt = new AboutCompany();
            rt.ShowDialog();
        }

        /// <summary>
        /// showing the client information by clicking on the button
        /// </summary>
        private void ShowClientButton_Click(object sender, EventArgs e)
        {
            AboutCompany rt = new AboutCompany();
            rt.ShowDialog();
        }

        /// <summary>
        /// showing the developers information by clicking on the button
        /// </summary>        
        private void ShowUsButton_Click(object sender, EventArgs e)
        {
       
            AboutUs rt = new AboutUs();
            rt.ShowDialog();
        }
    }
}
