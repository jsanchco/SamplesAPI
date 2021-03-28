using ConsoleApp.Authentication.Models;
using ConsoleApp.Authentication.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Authentication
{
    public class ConsoleApp : IHostedService
    {
        private readonly Configuration<AppConfig> _configuration;
        private readonly HMACService _hMACService;
        private readonly ILogger<ConsoleApp> _logger;

        public ConsoleApp(
            Configuration<AppConfig> configuration,
            HMACService hMACService,
            ILogger<ConsoleApp> logger)
        {
            _configuration = configuration;
            _hMACService = hMACService;
            _logger = logger;
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
            Console.WriteLine("Press (F1) to Test Authentication HMAC (Device1).");
            Console.WriteLine("Press (F2) to Test Authentication HMAC (Device2).");
            Console.WriteLine("Press (F3) to Test Authentication HMAC (Fake).");
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
                        await _hMACService.GetEcho("Hello", "Device1");
                        break;

                    case ConsoleKey.F2:
                        Console.WriteLine("Pressed F2 ...");
                        await _hMACService.GetEcho("Hello", "Device2");
                        break;

                    case ConsoleKey.F3:
                        Console.WriteLine("Pressed F3 ...");
                        await _hMACService.GetEcho("Hello", "Device3");
                        break;
                }
            } while (key != ConsoleKey.Escape);
        }
    }
}
