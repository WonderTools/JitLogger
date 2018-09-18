using System;
using Microsoft.Extensions.Logging;

namespace WonderTools.JitLogger
{
    public class JitLogger : ILogger
    {
        private readonly JitLogRepository repository;

        public JitLogger(JitLogRepository repository)
        {
            this.repository = repository;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new Disposable();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var logMessage = formatter.Invoke(state, exception);
            repository.AddLog(logLevel, eventId, logMessage, DateTime.Now);
        }

        public class Disposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}