using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Linq;

namespace CleanArchitecture.API.Configuration
{
    public static class SerilogConfig
    {
        public static IHostBuilder CreateLoggerSerilog(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                            .ReadFrom.Configuration(hostingContext.Configuration)
                            .TryAddApplicationInsights()
                            .AddFilters()
            );

            return hostBuilder;
        }

        private static LoggerConfiguration TryAddApplicationInsights(this LoggerConfiguration loggerConfiguration)
        {
            var appInsightsTelemetryConfiguration = TelemetryConfiguration.CreateDefault();
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var instrumentationKey = configuration.GetSection("ApplicationInsights:InstrumentationKey").Value;
            appInsightsTelemetryConfiguration.InstrumentationKey = instrumentationKey;

            if (!string.IsNullOrEmpty(instrumentationKey))
            {
                loggerConfiguration
                    .WriteTo.ApplicationInsights(
                                appInsightsTelemetryConfiguration,
                                TelemetryConverter.Traces);
            }

            return loggerConfiguration;
        }

        private static LoggerConfiguration AddFilters(this LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration
                .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("health")))
                .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger")));

            return loggerConfiguration;
        }
    }
}
