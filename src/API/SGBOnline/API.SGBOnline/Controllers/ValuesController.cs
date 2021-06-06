using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.CrmSdk.Interfaces;
using Shared.Model.Response;
using System;
using System.Net;

namespace API.Basic.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IContext _context;

        public ValuesController(
            ILogger<ValuesController> logger,
            IContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(string echo)
        {
            try
            {
                _logger.LogInformation($"In ValuesController -> [HttpGet]");

                var result = _context.Test(echo);
                return Ok(new ResponseResult<string>
                {
                    Succesful = true,
                    Data = result
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
