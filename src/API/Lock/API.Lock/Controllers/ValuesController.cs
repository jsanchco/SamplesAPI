using API.Lock.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Model.Response;
using System.Threading.Tasks;

namespace API.Lock.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IDoSomethingService _doSomethingService;

        public ValuesController(
            ILogger<ValuesController> logger,
            IDoSomethingService doSomethingService)
        {
            _logger = logger;
            _doSomethingService = doSomethingService;
        }

        [HttpGet]
        public IActionResult Get(string echo)
        {
            _logger.LogInformation($"In ValuesController -> [HttpGet]");

            return Ok(new ResponseResult<string>
            {
                Succesful = true,
                Data = echo
            });
        }

        [HttpGet("dosomething")]
        public IActionResult DoSomething()
        {
            _logger.LogInformation($"In ValuesController [DoSomething] -> [HttpGet]");

            var result = _doSomethingService.DoSomething();
            return Ok(result);
        }

        [HttpGet("dosomethingasync")]
        public async Task<IActionResult> DoSomethingAsync()
        {
            _logger.LogInformation($"In ValuesController [DoSomethingAsync] -> [HttpGet]");

            var result = await _doSomethingService.DoSomethingAsync();
            return Ok(result);
        }
    }
}
