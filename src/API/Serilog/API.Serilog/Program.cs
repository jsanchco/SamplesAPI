using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace API.Serilog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var appInsightsTelemetryConfiguration = TelemetryConfiguration.CreateDefault();
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var instrumentationKey = configuration.GetSection("ApplicationInsights:InstrumentationKey").Value;
            appInsightsTelemetryConfiguration.InstrumentationKey = instrumentationKey;

            return Host
                    .CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    })
                    .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                            .ReadFrom.Configuration(hostingContext.Configuration)
                            .WriteTo.ApplicationInsights(
                                appInsightsTelemetryConfiguration,
                                TelemetryConverter.Traces)
                    );
        }


        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        })
        //       .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
        //                .ReadFrom.Configuration(hostingContext.Configuration)
        //                .WriteTo.ApplicationInsights(
        //                    new TelemetryConfiguration { InstrumentationKey = "c045c8bc-3bf0-48d7-9bbc-33ca494e124e;IngestionEndpoint=https://southcentralus-0.in.applicationinsights.azure.com/" },
        //                    TelemetryConverter.Traces)
        //         );
    }
}
