using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;

namespace API.Serilog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(string echo)
        {
            _logger.LogInformation($"In ValuesController [GET] -> {JsonConvert.SerializeObject(echo)}");

            return Ok(echo);
        }
    }
}
