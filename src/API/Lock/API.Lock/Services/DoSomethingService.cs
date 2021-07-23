using API.Lock.Models;
using Microsoft.Extensions.Logging;
using Shared.Model.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Lock.Services
{
    public class DoSomethingService : IDoSomethingService
    {
        private readonly ILogger<DoSomethingService> _logger;

        private readonly object _transaction = new object();

        public DoSomethingService(ILogger<DoSomethingService> logger)
        {
            _logger = logger;
        }

        public ResponseResult<DoSomethingDTO> DoSomething()
        {
            var start = DateTime.Now;
            _logger.LogInformation($"Before ... Start -> [{DateTime.Now:HH:mm:ss}]");

            lock (_transaction)
            {
                _logger.LogInformation($"After ... Start -> [{DateTime.Now:HH:mm:ss}]");

                Thread.Sleep(20000);

                return new ResponseResult<DoSomethingDTO>
                {
                    Succesful = true,
                    Data = new DoSomethingDTO
                    {
                        Type = "NOT Async",
                        Start = start,
                        Stop = System.DateTime.Now
                    }
                };
            }
        }

        public async Task<ResponseResult<DoSomethingDTO>> DoSomethingAsync()
        {
            var start = DateTime.Now;

            await Task.Delay(5000);

            return new ResponseResult<DoSomethingDTO>
            {
                Succesful = true,
                Data = new DoSomethingDTO
                {
                    Type = "YES Async",
                    Start = start,
                    Stop = System.DateTime.Now
                }
            };
        }
    }
}
