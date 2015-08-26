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
        public void Log(string message, LogType logType)
        {
            GlobalContext.Properties["LogType"] = logType.ToString();
            
            log.Info(message);
        }

        public List<string> GetLog(DateTime from, DateTime to)
        {
            string route = (log4net.LogManager.GetCurrentLoggers()[0].Logger.Repository.GetAppenders()[0] as FileAppender).File;
            return null;
        }

    }
}
