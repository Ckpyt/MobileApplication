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
    public partial class LogsPage :
#if DEBSYMB
        Form
#else
        TabPage
#endif
    {
        public LogsPage()
        {
            InitializeComponent();
        }
    }
}
