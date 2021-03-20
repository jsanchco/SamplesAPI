using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Model.Response;
using System;
using System.Net;

namespace API.Basic.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]   
    [Authorize(AuthenticationSchemes = "HMAC")]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{echo}")]
        public IActionResult Get(string echo)
        {
            try
            {
                _logger.LogInformation($"In ValuesController -> [HttpGet]");

                return Ok(new ResponseResult<string>
                {
                    Succesful = true,
                    Data = echo
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"In ValuesController -> [HttpGet]");
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
