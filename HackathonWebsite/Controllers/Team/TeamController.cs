using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers.Team
{
    [ApiController]
    [Route("team")]
    public class TeamController : ControllerBase
    {
        [HttpPost("send-invite")]
        public async Task<IActionResult> SendInvite()
        {
            return Ok();
        }
    }
}
