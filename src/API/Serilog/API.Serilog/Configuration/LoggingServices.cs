using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Shared.Logger;

namespace API.Serilog.Configuration
{
    public static class LoggingServices
    {
        //// Check url -> https://my.papertrailapp.com/events        
        public static IServiceCollection AddPapertrailLogging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(builder =>
            {
                builder.AddProvider(new SyslogLoggerProvider
                    (
                        configuration.GetValue<string>("Papertrail:host"),
                        configuration.GetValue<int>("Papertrail:port"),
                        null
                    ));
            });

            return services;
        }

        public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();
            });
            return services;
        }
    }
}
