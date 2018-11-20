using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unified_dot_program
{
    public class Logger
    {
        private static string LOG_TIME_FORMAT = "H:mm:ss:fff";
        private static string LOG_DATE_FORMAT = "MM/dd/yyyy";
        public static string mode;
        public static string effect;
        public static string parNum;

        public int samplingInterval;
        public string logPath;
        public string fileName;
        
        
        public int r; // data point identifier
        

        public Logger(string logPath, string logType)
        {
            this.logPath = logPath;
            string s = System.DateTime.Now.ToString("yyyy_MM_dd_Hmmss");
            fileName = logPath + "/" + logType + "_" + mode + effect + "_" + s + ".csv";
            samplingInterval = Convert.ToInt32(Statics.XML.Root.Element("all").Element("samplingInterval").Value);

        }



        public static void Initialize()
        {
            parNum = "";
            mode = "";
            effect = "";
        }

        public void Begin()
        {        
            StreamWriter textOut = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write));
            textOut.WriteLine("Trial initialized at " + System.DateTime.Now.ToString(LOG_TIME_FORMAT + " on " + LOG_DATE_FORMAT));
            textOut.Close();
        }

        public void Close()
        {
            Console.WriteLine("Logger closed");
            Output("Trial closed at " + System.DateTime.Now.ToString(LOG_TIME_FORMAT + " on " + LOG_DATE_FORMAT));
        }

        private void Output(string text)
        {
            try
            {
                StreamWriter textOut = new StreamWriter(new FileStream(fileName, FileMode.Append, FileAccess.Write));
                textOut.WriteLine("[" + System.DateTime.Now.ToString(LOG_TIME_FORMAT) + "] " + text);
                textOut.Close();
            }
            catch (System.Exception e)
            {
                string error = e.Message;
            }
        }

        public void Log(string message)
        {
            Output(message);
        }

    }
}
