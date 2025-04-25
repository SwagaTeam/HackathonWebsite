using HackathonWebsite.DataLayer.Repositories.Implementations;
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
            //var link = await _projectLinkManager.Create(request.ProjectId);

            //link = $"{Request.Scheme}://{Request.Host}/project/invite/{link}";

            //var project = await _projectManager.GetById(request.ProjectId);

            //var user = await UserRepository.GetUserByEmail(request.Email);

            //if (user is null || project is null)
            //    return BadRequest("Ïîëüçîâàòåëÿ èëè ïðîåêòà íå ñóùåñòâóåò");

            //await _emailSender.SendEmailAsync(
            //    user.Email,
            //    "Ïðèãëàøåíèå â ïðîåêò",
            //    $"Çäðàâñòâóéòå, {user.Username}, âàñ ïðèãëàøàþò â ïðîåêò {project.Name}.\n" +
            //    $"Ññûëêà-ïðèãëàøåíèå: {link}");

            return Ok();
        }
    }
}
