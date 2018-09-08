using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Nachiappan.JitLogger
{
    public class JitLoggingMiddleware
    {
        public JitLoggingMiddleware(JitLogRepository jitLogRepository)
        {
            _jitLogRepository = jitLogRepository;
        }

        private JitLogRepository _jitLogRepository;

        public async Task Process(HttpContext context, Func<Task> next)
        {
            var path = context.Request.Path;
            if (IsRequestForJitLogs(path)) await HandleJitLogsRequest(context);
            else await next.Invoke();
        }

        private async Task HandleJitLogsRequest(HttpContext context)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            var logs = _jitLogRepository.GetLogs();
            var jsonString = JsonConvert.SerializeObject(logs);
            await context.Response.WriteAsync(jsonString, Encoding.UTF8);
        }

        private bool IsRequestForJitLogs(string path)
        {
            var jitLogsPath = "/jit-logger/logs";
            if (string.IsNullOrWhiteSpace(path)) return false;
            if (path.Equals(jitLogsPath, StringComparison.InvariantCultureIgnoreCase)) return true;
            if (path.Equals(jitLogsPath+"/", StringComparison.InvariantCultureIgnoreCase)) return true;
            return false;
        }
    }
}