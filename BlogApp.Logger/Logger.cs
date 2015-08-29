using BlogApp.ILogger;
using BlogApp.Utilities;
using log4net;
using log4net.Appender;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BlogApp.Logger
{
    public class Logger: BlogApp.ILogger.ILogger
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ILogDataAccess logDataAccess;
        public Logger(ILogDataAccess logDataAcces)
        {
            this.logDataAccess = logDataAcces; 
        }
        public void Log(string message, LogType logType, int userId)
        {
            GlobalContext.Properties["LogType"] = ((int)logType).ToString();
            GlobalContext.Properties["UserId"] = userId.ToString();
            
            log.Info(message);
        }

        public List<string> GetLog(DateTime from, DateTime to)
        {
            
            return null;
        }

         
    }
}
