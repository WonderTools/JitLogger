using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WonderTools.JitLogger;
using Newtonsoft.Json;

namespace WonderTools.JitLogger
{
    public class JitLoggingMiddleware
    {
        public JitLoggingMiddleware(JitLogRepository jitLogRepository, JitLoggerOptions options)
        {
            _jitLogRepository = jitLogRepository;
            _options = options;
        }

        private JitLogRepository _jitLogRepository;
        private readonly JitLoggerOptions _options;

        public async Task Process(HttpContext context, Func<Task> next)
        {
            var path = context.Request.Path;
            var method = context.Request.Method;
            if (IsRequestForJitLogs(path, method)) await HandleJitLogsRequest(context);
            else if (IsRequestForJitUi(path, method)) await HandleJitUiRequest(context);
            else if (IsRequestForPreflightJitUi(path, method) && _options.IsCorsEnabled) await HandlePreflight(context);
            else if (IsRequestForPreflightJitLogs(path, method) && _options.IsCorsEnabled) await HandlePreflight(context);
            else await next.Invoke();
        }

        private async Task HandleJitUiRequest(HttpContext context)
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/html";
            var html = HtmlGenerator.GetHtml(_options.LoggerName, _jitLogRepository.GetLogs());
            await context.Response.WriteAsync(html, Encoding.UTF8);
        }

        private async Task HandlePreflight(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
            context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "authorization", "cache-control" });
            context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "null" });
            context.Response.Headers.Add("Vary", new[] { "Origin" });
            context.Response.StatusCode = 204;
            await context.Response.WriteAsync(string.Empty, Encoding.UTF8);
        }
         
        private async Task HandleJitLogsRequest(HttpContext context)
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            var logs = _jitLogRepository.GetLogs();

            logs = FilterLogsBasedOnQueryString(context, logs);

            var logsModel = new LogsModel()
            {
                Name = _options.LoggerName,
                Logs = logs,
            };

            var jsonString = JsonConvert.SerializeObject(logsModel);
            await context.Response.WriteAsync(jsonString, Encoding.UTF8);
        }

        private static List<Log> FilterLogsBasedOnQueryString(HttpContext context, List<Log> logs)
        {
            var QueryParameter = "exclusion-log-id-limit";
            if (!context.Request.Query.ContainsKey(QueryParameter)) return logs;
            var idAsString = context.Request.Query[QueryParameter].ToString();
            if (!Int32.TryParse(idAsString, out var id)) return logs;
            if (!logs.Any(x => x.LogId == id)) return logs;
            return logs.Where(x => x.LogId > id).ToList();
        }

        private bool IsRequestForJitUi(string path, string method)
        {
            return IsRequestValid(path,method, "/ui", "get");
        }

        private bool IsRequestForJitLogs(string path, string method)
        {
            return IsRequestValid(path,method, "/logs","get");
        }

        private bool IsRequestForPreflightJitUi(string path, string method)
        {
            return IsRequestValid(path, method, "/ui", "options");
        }

        private bool IsRequestForPreflightJitLogs(string path, string method)
        {
            return IsRequestValid(path, method, "/logs", "options");
        }

        private bool IsRequestValid(string requestPath, string requestMethod, string expectedAdditionalPath, string expectedMethod)
        {
            var path = _options.JitEndPointBaseUrl + expectedAdditionalPath;
            if (string.IsNullOrWhiteSpace(requestPath)) return false;
            if (string.IsNullOrEmpty(requestMethod)) return false;
            if (!requestMethod.Equals(expectedMethod, StringComparison.InvariantCultureIgnoreCase)) return false;
            if (requestPath.Equals(path, StringComparison.InvariantCultureIgnoreCase)) return true;
            if (requestPath.Equals(path + "/", StringComparison.InvariantCultureIgnoreCase)) return true;
            return false;
        }
    }

    public class LogsModel
    {
        public string Name { get; set; }
        public List<Log> Logs { get; set; }
    }
}