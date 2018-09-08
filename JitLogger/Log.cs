using System;
using Microsoft.Extensions.Logging;

namespace Nachiappan.JitLogger
{
    public class Log
    {
        public Log(LogLevel logLevel, EventId eventId, string logMessage, DateTime time)
        {
            LogLevel = logLevel;
            EventId = eventId;
            LogMessage = logMessage;
            DateTime = time;
        }

        public LogLevel LogLevel { get; set; }
        public EventId EventId { get; set; }
        public string LogMessage { get; set; }
        public DateTime DateTime { get; set; }
    }
}