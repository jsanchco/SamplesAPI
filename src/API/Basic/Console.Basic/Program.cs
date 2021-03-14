using ConsoleApp.Basic.Models;
using ConsoleApp.Basic.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Logger;
using System.Threading.Tasks;

namespace ConsoleApp.Basic
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);

            await hostBuilder.RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddSingleton<Configuration<AppConfig>>();
                    services.AddSingleton<IHostedService, ConsoleApp>();

                    services.AddHttpClient();

                    // Add Papertrail to trace
                    var serviceProvider = services.BuildServiceProvider();
                    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                    var configuration = new Configuration<AppConfig>();
                    loggerFactory.AddSyslog(
                        configuration.Settings.Papertrail.host,
                        configuration.Settings.Papertrail.port);
                    services.AddSingleton(typeof(ILoggerFactory), loggerFactory);
                    // Add Papertrail to trace
                });
    }
}
