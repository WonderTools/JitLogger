namespace WonderTools.JitLogger
{
    public class JitLoggerOptions
    {
        public JitLoggerOptions()
        {
            LoggerName = "Jit Logger";
            LogRetentionTimeInSeconds = 300;
            LogRetentionBufferSize = 500;
            JitEndPointBaseUrl = "/jit-logger";
            IsCorsEnabled = true;
        }

        public string JitEndPointBaseUrl { get; set; }
        public string LoggerName { get; set; }
        public int LogRetentionTimeInSeconds { get; set; }
        public int LogRetentionBufferSize { get; set; }
        public bool IsCorsEnabled { get; set; }
    }
}