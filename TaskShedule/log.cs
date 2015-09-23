using System;
using System.IO;

namespace TaskShedule
{
    public static class log
    {
        public static void LOG(string msg)
        {
            using (StreamWriter w = File.AppendText(@"c:\TaskShedule\log.txt"))
            {
                Log(msg, w);
            }
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }
    }
}
