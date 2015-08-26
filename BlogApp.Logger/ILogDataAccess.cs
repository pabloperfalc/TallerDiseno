
using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Logger
{
    public interface ILogDataAccess
    {
        List<LogEntry> GetLog(DateTime fromDate, DateTime toDate);
    }
}
