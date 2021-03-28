using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Model.Response;
using System;
using System.Net;

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
            try
            {
                _logger.LogInformation($"In TestController -> [HttpGet]");

                return Ok(new ResponseResult<string>
                {
                    Succesful = true,
                    Data = echo
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"In TestController -> [HttpGet]");
                return BadRequest(new ResponseResult<string>
                {
                    Succesful = false,
                    Error = new Error
                    {
                        Code = (int)HttpStatusCode.InternalServerError,
                        Message = $"Execption in [HttpGet]",
                        Description = ex.Message
                    }
                });
            }
        }
    }
}
