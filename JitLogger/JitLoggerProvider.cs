using Microsoft.Extensions.Logging;

namespace WonderTools.JitLogger
{
    [ProviderAlias("Jit")]
    public class JitLoggerProvider : ILoggerProvider
    {
        private readonly JitLogRepository repository;

        public JitLoggerProvider(JitLogRepository repository)
        {
            this.repository = repository;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new JitLogger(repository);
        }

        public void Dispose()
        {
        }
    }
}