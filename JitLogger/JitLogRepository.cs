using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Nachiappan.JitLogger
{
    public class JitLogRepository
    {
        private readonly JitLoggerOptions _options;
        private int logId = 1;

        public JitLogRepository(JitLoggerOptions options)
        {
            _options = options;
        }
        
        List<Log> _bufferedLogs = new List<Log>();
        Queue<Log> _logss = new Queue<Log>();

        public void AddLog(LogLevel logLevel, EventId eventId, string logMessage, DateTime time)
        {
            var item = new Log(logId, logLevel, eventId, logMessage, time);
            logId++;
            var timeUpperLimit = time.Subtract(new TimeSpan(0, 0, _options.LogRetentionTimeInSeconds));

            lock (_logss)
            {
                _logss.Enqueue(item);
                while (_logss.Count > _options.LogRetentionBufferSize)
                    _logss.Dequeue();
                while ((_logss.Count > 0) && (_logss.ElementAt(0).DateTime < timeUpperLimit) )
                    _logss.Dequeue();
            }

            lock (_bufferedLogs)
            {
                _bufferedLogs.Clear();
                _bufferedLogs.AddRange(_logss);
            }
        }

        public List<Log> GetLogs()
        {
            lock (_bufferedLogs)
            {
                return _bufferedLogs;
            }
        }
    }
}