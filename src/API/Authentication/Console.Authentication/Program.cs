using ConsoleApp.Authentication.Models;
using ConsoleApp.Authentication.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Authentication.Services;
using Shared.Logger;
using System.Threading.Tasks;

namespace ConsoleApp.Authentication
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

                    services.AddSingleton<HMACService>();
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

                    services.AddScoped<ISecretLookup>(x => new SecretLookup(
                    null,
                    new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 }));
                });
    }
}
