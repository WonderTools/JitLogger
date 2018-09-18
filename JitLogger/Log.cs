using System;
using Microsoft.Extensions.Logging;

namespace WonderTools.JitLogger
{
    public class Log
    {
        public Log(int logid, LogLevel logLevel, EventId eventId, string logMessage, DateTime time)
        {
            LogId = logid;
            LogLevel = logLevel;
            EventId = eventId;
            LogMessage = logMessage;
            DateTime = time;
        }

        public int LogId { get; set; }
        public LogLevel LogLevel { get; set; }
        public EventId EventId { get; set; }
        public string LogMessage { get; set; }
        public DateTime DateTime { get; set; }
    }
}