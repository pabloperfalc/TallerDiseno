using BlogApp.ILogger;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Logger
{
    internal class LogContext : DbContext
    {
        public DbSet<LogEntry> Logs { get; set; }
    }
}
