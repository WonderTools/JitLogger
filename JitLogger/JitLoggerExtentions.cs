using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Nachiappan.JitLogger
{
    public static class JitLoggerExtentions
    {
        public static void AddJitLogger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<JitLogRepository>();
            serviceCollection.AddTransient<JitLoggingMiddleware>();
            serviceCollection.AddSingleton<JitLoggerOptions>();

        }

        public static IApplicationBuilder UseJitLogger
            (this IApplicationBuilder builder, ILoggerFactory loggerFactory, Action<JitLoggerOptions> configureAction = null)
        {
            
            var repository = Get<JitLogRepository>(builder);
            ConfigureOptions(builder, configureAction);

            loggerFactory.AddProvider(new JitLoggerProvider(repository));

            var loggingMiddleware = builder.ApplicationServices.GetService<JitLoggingMiddleware>();
            builder.Use(loggingMiddleware.Process);
            return builder;
        }

        private static void ConfigureOptions(IApplicationBuilder builder, Action<JitLoggerOptions> configureAction)
        {
            if (configureAction != null)
            {
                var options = Get<JitLoggerOptions>(builder);
                configureAction.Invoke(options);

                if (string.IsNullOrWhiteSpace(options.LoggerName))
                    throw new Exception("The Jit Logger name in options in not valid");

                if (!IsEnPointValid(options.JitEndPointBaseUrl))
                {
                    var error1 = "should begin in /";
                    var error2 = "should be followed by an alphabet";
                    var error3 = "should contain only alpha-numeric character in smaller case";
                    var error4 = "should have at least 3 characters";
                    var error5 = "should not end with /";
                    throw new Exception($"The end point {error1}, {error2}, {error3}, {error4} and {error5}");
                }

                if (options.LogRetentionTimeInSeconds < 0) throw new Exception("Log retention time can not be negative");
                if (options.LogRetentionBufferSize < 0) throw new Exception("Log buffer size can not be negative");
            }
        }

        private static bool IsEnPointValid(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return false;
            if (path.Length < 3) return false;
            if (!Regex.IsMatch(path, @"^[/][a-z][-a-z0-9/]+$")) return false;
            if (path.EndsWith("/")) return false;
            return true;
        }

        private static T Get<T>(IApplicationBuilder builder)
        {
            var errorMessage = "Jit logger has to be added while configuring service.";
            var t = builder.ApplicationServices.GetService<T>();
            if (t == null) throw new Exception(errorMessage);
            return t;
        }
    }
}