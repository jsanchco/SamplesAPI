using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Model.Response;

namespace API.Basic.Controllers
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
            _logger.LogInformation($"In ValuesController -> [HttpGet]");

            return Ok(new ResponseResult<string>
            {
                Succesful = true,
                Data = echo
            });
        }
    }
}
