using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Management;
using Bsc.Dmtds.Common.IO;

namespace Bsc.Dmtds.Web.HealthMonitoring
{
    public static class TextFileLogger
    {
        private static string WebEventsDir = "WebEvents";
        private static object lockerHelper = new object();
        public static void Log(string message)
        {
            lock (lockerHelper)
            {
                var fileName = GetLogFile();

                IOUtility.EnsureDirectoryExists(Path.GetDirectoryName(fileName));

                File.AppendAllText(fileName, message.Replace("\n", Environment.NewLine));
                File.AppendAllText(fileName, "--------------------------------------------------------------------------------------------------------\r\n");
            }
        }
        private static string GetLogFile()
        {
            string filePath = "/Log/abc.txt";
            return HttpContext.Current.Server.MapPath(filePath);
        }
        private static string GetLogFile(string baseDir)
        {
            var webEventsDir = Path.Combine(baseDir, WebEventsDir);
            var filePath = Path.Combine(webEventsDir, DateTime.UtcNow.ToString("yyyy-MM-dd") + ".log");
            return filePath;
        }
    }


    public class TextFileWebEventProvider : WebEventProvider
    {
        // Methods
        public TextFileWebEventProvider()
        {
        }

        public override void Flush()
        {
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
        }

        public override void ProcessEvent(WebBaseEvent eventRaised)
        {
            TextFileLogger.Log(eventRaised.ToString());
        }

        public override void Shutdown()
        {

        }

    }
}