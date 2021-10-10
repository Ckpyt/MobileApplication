using System;
using System.Windows.Forms;

namespace MobileApplication
{
    public partial class AboutUs : Form
    {
        public AboutUs()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
