using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Model.Response;

namespace API.SerilogWithApplicationInsights.Controllers
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
            _logger.LogInformation($"In ValuesController -> [HttpGet], Request [{JsonConvert.SerializeObject(echo)}]");

            return Ok(new ResponseResult<string>
            {
                Succesful = true,
                Data = echo
            });
        }
    }
}
