﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.ILogger
{
    public interface ILogger
    {
        void Log(string message, LogType logType);
        List<string> GetLog(DateTime from, DateTime to);
    }
}
