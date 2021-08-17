using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.ROP.Interfaces;
using Shared.DTO.ROP;
using System.Threading.Tasks;

namespace API.ROP.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonalProfileController : ControllerBase
    {
        private readonly ILogger<PersonalProfileController> _logger;
        private readonly IPostPersonalProfile _postPersonalProfile;

        public PersonalProfileController(
            ILogger<PersonalProfileController> logger,
            IPostPersonalProfile postPersonalProfile)
        {
            _logger = logger;
            _postPersonalProfile = postPersonalProfile;
        }

        [HttpPost("addpersonalprofile")]
        public async Task<IActionResult> AddPersonalProfile(PersonalProfileDto personalProfileDto)
        {
            _logger.LogInformation($"In PersonalProfileController -> [HttpPost(addpersonalprofile)]");

            var result = await _postPersonalProfile.AddPersonalProfile(personalProfileDto);

            return Ok(result);
        }

        [HttpPost("addpersonalprofileandsendemail")]
        public async Task<IActionResult> AddPersonalProfileAndSendEmail(PersonalProfileDto personalProfileDto)
        {
            _logger.LogInformation($"In PersonalProfileController -> [HttpPost(addpersonalprofileandsendemail)]");

            var result = await _postPersonalProfile.AddPersonalProfileAndSendEmail(personalProfileDto);

            return Ok(result);
        }
    }
}
