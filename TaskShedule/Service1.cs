using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Xml;
using System.Timers;

namespace TaskShedule
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += new ElapsedEventHandler(timer_Tick);
            timer.Interval = 10000;
            timer.Enabled = true;
            log.LOG("comecei");
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
            log.LOG("parei");
        }

        private void timer_Tick(object source, ElapsedEventArgs e)
        {
            log.LOG("comecei os ticks");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"c:\TaskShedule\tt.xml");
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/Task/App");
            string appID = "", appName = "", appPath = "", appRunTime = "", appLastRunTime = "", appRunTimeCount = "";
            foreach (XmlNode node in nodeList)
            {
                appID = node.SelectSingleNode("id").InnerText;
                appName = node.SelectSingleNode("name").InnerText;
                appPath = node.SelectSingleNode("path").InnerText;
                appRunTime = node.SelectSingleNode("runtime").InnerText;
                appLastRunTime = node.SelectSingleNode("lastruntime").InnerText;
                appRunTimeCount = node.SelectSingleNode("runtimecount").InnerText;

                DateTime dtnow = DateTime.Now;
                DateTime dtruntime = Convert.ToDateTime(appRunTime);
                DateTime dtlastruntime = Convert.ToDateTime(appLastRunTime);
                int runtimecount = Convert.ToInt32(appRunTimeCount);

                try
                {
                    if ((dtnow >= dtruntime) && (dtlastruntime < dtruntime) && (dtnow != dtlastruntime))
                    {
                        string i = (runtimecount + 1).ToString();
                        node.SelectSingleNode("runtimecount").InnerText = i;
                        node.SelectSingleNode("lastruntime").InnerText = dtnow.ToString();
                        xmlDoc.Save(@"c:\TaskShedule\tt.xml");
                        Process.Start(appPath);
                    }
                }
                catch (Exception ex) { log.LOG(ex.ToString()); }
            }
        }
    }
}
