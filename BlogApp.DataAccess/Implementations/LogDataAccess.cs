
using BlogApp.Logger;
using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess.Implementations
{
    public class LogDataAccess : ILogDataAccess
    {
      
        public List<LogEntry> GetLog(DateTime fromDate, DateTime toDate)
        {
            using (var db = new BlogContext())
            {
                var query = (from u in db.LogEntries
                            where u.Date <= toDate && u.Date >= fromDate
                             select u).ToList();

                return query;
            }
        }
    }
}
