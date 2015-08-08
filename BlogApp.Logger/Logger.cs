using BlogApp.Utilities;
using log4net.Appender;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Logger
{
    public class Logger
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Log(string message)
        {
            log.Info(message);
        }

        public List<string> GetLog(DateTime from, DateTime to)
        {
            string route = (log4net.LogManager.GetCurrentLoggers()[0].Logger.Repository.GetAppenders()[0] as FileAppender).File;
            return ReadFile(route, from, to);
        }

        private List<string> ReadFile(string route, DateTime from, DateTime to)
        {
            try
            {
                Stream stream = File.Open(route, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader file = new StreamReader(stream);
                string line = file.ReadLine();
                List<string> result = new List<string>();
                while (line != null)
                {
                    string[] strParams = line.Split(new string[] { " INFO " }, StringSplitOptions.None);
                    DateTime date = DateTime.Parse(strParams[0].Substring(0, 19));
                    string userinfo = strParams[1];
                    if (from.Date <= date.Date && date.Date <= to.Date)
                    {
                        string strLog = date.ToString() + " " + userinfo;
                        result.Add(strLog);
                    }
                    line = file.ReadLine();
                }
                return result;
            }
            catch (FileNotFoundException)
            {
                throw new BlogException("No se ha podido encontrar el archivo de log.");
            }
            catch (Exception)
            {
                throw new BlogException("Ha ocurrido un error al leer el archivo de log.");
            }
        }
    }
}
