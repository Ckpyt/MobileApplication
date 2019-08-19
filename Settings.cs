using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobileApplication
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            textBox1.Text = configuration.AppSettings.Settings["dataBaseFile"].Value;
            textBox2.Text = configuration.AppSettings.Settings["invoiceFile"].Value;
            textBox3.Text = configuration.AppSettings.Settings["outputDirectory"].Value;
            
            
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["dataBaseFile"].Value = textBox1.Text;
            configuration.AppSettings.Settings["invoiceFile"].Value = textBox2.Text;
            configuration.AppSettings.Settings["outputDirectory"].Value = textBox3.Text;

            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");


            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = textBox1.Text;
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                if (File.Exists(file))
                    textBox1.Text = file;
                else
                    MessageBox.Show("Sorry, the file could not be founded", "File error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = textBox2.Text;
            var result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                if (File.Exists(file))
                    textBox2.Text = file;
                else
                    MessageBox.Show("Sorry, the file could not be founded", "File error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBox3.Text;
            var result = folderBrowserDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                string dir = folderBrowserDialog1.SelectedPath;
                if (Directory.Exists(dir))
                {
                    textBox3.Text = dir;
                }
                else
                {
                    MessageBox.Show("Sorry, the directory could not be founded", "Directory error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
