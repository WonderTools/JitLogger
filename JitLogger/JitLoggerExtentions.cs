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
            
        }

        public static IApplicationBuilder UseJitLogger
            (this IApplicationBuilder builder, ILoggerFactory loggerFactory)
        {
            var repostiory = builder.ApplicationServices.GetService<JitLogRepository>();
            loggerFactory.AddProvider(new JitLoggerProvider(repostiory));
            var loggingMiddleware = builder.ApplicationServices.GetService<JitLoggingMiddleware>();
            builder.Use(loggingMiddleware.Process);
            return builder;
        }
    }
}