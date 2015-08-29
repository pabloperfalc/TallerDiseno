using BlogApp.ILogger;
using BlogApp.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Logger
{
    public class LogDataAccess
    {    
        public List<ILogEntry> GetLog(DateTime fromDate, DateTime toDate)
        {
            using (var lb = new LogContext())
            {
                var query = (from l in lb.Logs
                            where l.Date <= toDate && l.Date >= fromDate
                             select l).ToList<ILogEntry>();

                return query;
            }
        }
    }
}
