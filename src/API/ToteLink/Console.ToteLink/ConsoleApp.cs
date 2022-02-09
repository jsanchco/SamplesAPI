using ConsoleApp.ToteLink.Models;
using ConsoleApp.ToteLink.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.ToteLink
{
    public class ConsoleApp : IHostedService
    {
        private readonly Configuration<AppConfig> _configuration;
        private readonly ILogger<ConsoleApp> _logger;
        private readonly IHostApplicationLifetime _lifeTime;
        private readonly ToteLinkService _toteLinkService;

        public ConsoleApp(
            Configuration<AppConfig> configuration,
            ILogger<ConsoleApp> logger,
            IHostApplicationLifetime lifeTime,
            ToteLinkService toteLinkService)
        {
            _configuration = configuration;
            _logger = logger;
            _lifeTime = lifeTime;
            _toteLinkService = toteLinkService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            PrintTitle();
            await Process();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void PrintTitle()
        {
            var lengthTotal = _configuration.Settings.Title.Length + 20;
            Console.WriteLine($"{new string('*', lengthTotal)}");
            Console.WriteLine($"*         {_configuration.Settings.Title}         *");
            Console.WriteLine($"{new string('*', lengthTotal)}");

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press (F1) to Test PuntoPagoIngresarCredito_Local");
            Console.WriteLine("Press (F2) to Test PuntoPagoIngresarCredito_Azure");
            Console.WriteLine("Press (ESC) to exit");
            Console.WriteLine("");
        }

        private async Task Process()
        {
            ConsoleKey key;
            do
            {
                while (!Console.KeyAvailable)
                {
                    // Do something, but don't read key here
                }

                // Key is available - read it
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.F1:
                        Console.WriteLine("Pressed F1 ...");
                        _logger.LogInformation("Pressed F1 ...");
                        await _toteLinkService.PuntoPagoIngresarCredito_Local();

                        break;

                    case ConsoleKey.F2:
                        Console.WriteLine("Pressed F2 ...");
                        _logger.LogInformation("Pressed F2 ...");
                        await _toteLinkService.PuntoPagoIngresarCredito_Azure();

                        break;
                }
            } while (key != ConsoleKey.Escape);

            Console.WriteLine("Bye ...");
            Thread.Sleep(1000);
            _lifeTime.StopApplication();
        }
    }
}
