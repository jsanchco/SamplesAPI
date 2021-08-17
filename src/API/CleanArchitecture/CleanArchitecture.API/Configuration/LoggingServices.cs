using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Logger;

namespace API.Basic.Configuration
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
    }
}
