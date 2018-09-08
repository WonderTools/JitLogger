using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Nachiappan.JitLogger
{
    public class JitLogRepository
    {
        List<Log> logs = new List<Log>();
        public void AddLog(LogLevel logLevel, EventId eventId, string logMessage, DateTime time)
        {
            logs.Add(new Log(logLevel, eventId, logMessage, time));
        }

        public List<Log> GetLogs()
        {
            return logs;
        }
    }
}