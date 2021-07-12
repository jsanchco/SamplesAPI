using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.ROP.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonalProfileController : ControllerBase
    {
        private readonly ILogger<PersonalProfileController> _logger;

        public PersonalProfileController(
            ILogger<PersonalProfileController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(string echo)
        {
            _logger.LogInformation($"In PersonalProfileController -> [HttpPost]");

            return Ok();
        }
    }
}
