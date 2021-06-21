using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Model.Response;

namespace API.Authentication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = "HMAC1")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("echo/{echo}")]
        public IActionResult Get(string echo)
        {
            _logger.LogInformation($"In TestController -> [HttpGet]");

            return Ok(new ResponseResult<string>
            {
                Succesful = true,
                Data = echo
            });
        }
    }
}
