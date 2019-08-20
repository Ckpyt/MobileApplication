using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplication
{
    class Logger
    {
        static Logger logger;

        private Logger()
        {
            logger = this;
        }
        public static Logger GetInstance()
        {
            return logger == null ? new Logger() : logger;
        }

        public void SaveLog(string log)
        {
            string path = "app.log";
            StreamWriter logFl;

            logFl = File.AppendText(path);
            logFl.WriteLine( DateTime.Now.ToString("hh:mm:ss dd-MM-yyyy") + " " + log);
            logFl.Close();
        }
    }
}
