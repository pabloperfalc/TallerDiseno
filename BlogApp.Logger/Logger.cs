using BlogApp.ILogger;
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
        private readonly LogDataAccess logDataAccess;
      
        public Logger(LogDataAccess logDataAcces)
        {
            this.logDataAccess = logDataAcces; 
        }
        
        public void Log(string message, LogType logType, string userUsername)
        {
            GlobalContext.Properties["LogType"] = logType.ToString();
            GlobalContext.Properties["UserUsername"] = userUsername;
            
            log.Info(message);
        }

        public List<ILogEntry> GetLog(DateTime from, DateTime to)
        {
            return logDataAccess.GetLog(from, to);
        }

         
    }
}
