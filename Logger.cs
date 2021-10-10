using System;
using System.IO;

namespace MobileApplication
{
    /// <summary>
    /// class for storing logs works only in debug build
    /// </summary>
    class Logger
    {
        /// <summary> selflink for singletone </summary>
        static Logger logger;

        private Logger()
        {
            logger = this;
        }

        /// <summary>
        /// Part of singletone
        /// </summary>
        /// <returns> returns only one object </returns>
        public static Logger GetInstance()
        {
            return logger == null ? new Logger() : logger;
        }

        /// <summary>
        /// Save log into file
        /// </summary>
        /// <param name="log"> log string </param>
        public void SaveLog(string log)
        {
            //logging should works only for debugging
#if DEBSYMB
            string path = "app.log";
            StreamWriter logFl;

            logFl = File.AppendText(path);
            logFl.WriteLine( DateTime.Now.ToString("hh:mm:ss dd-MM-yyyy") + " " + log);
            logFl.Close();
#endif
        }
    }
}
