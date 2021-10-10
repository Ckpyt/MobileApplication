using System;
using System.Windows.Forms;

namespace MobileApplication
{
    public partial class AboutCompany : Form
    {
        public AboutCompany()
        {
            InitializeComponent();
         
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
