using Microsoft.AspNetCore.Mvc;

namespace API.Serilog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string echo)
        {
            //_logger.LogInformation($"In ValuesController -> [HttpGet]");

            return Ok(echo);
        }
    }
}
