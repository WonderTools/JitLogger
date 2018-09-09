namespace Nachiappan.JitLogger
{
    public class JitLoggerOptions
    {
        public JitLoggerOptions()
        {
            LoggerName = "Jit Logger";
            LogRetentionTimeInSeconds = 300;
            LogRetentionBufferSize = 500;
            JitEndPointPath = "/jit-logger";
        }

        public string JitEndPointPath { get; set; }
        public string LoggerName { get; set; }
        public int LogRetentionTimeInSeconds { get; set; }
        public int LogRetentionBufferSize { get; set; }
    }
}