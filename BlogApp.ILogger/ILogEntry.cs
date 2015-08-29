using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.ILogger
{
    public interface ILogEntry
    {
        int Id { get; set; }
        DateTime Date { get; set; }
        string Thread { get; set; }
        string Level { get; set; }
        string LogType { get; set; }
        string UserUsername { get; set; }
    }
}
