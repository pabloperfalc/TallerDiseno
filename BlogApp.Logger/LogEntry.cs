using BlogApp.ILogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Logger
{
    public class LogEntry : ILogEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string LogType { get; set; }
        public string UserUsername { get; set; }
    }
}
