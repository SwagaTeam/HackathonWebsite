using HackathonWebsite.BusinessLayer.Services.ApplyService;
using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.BusinessLayer.Services.MailService;
using HackathonWebsite.DTO;
using HackathonWebsite.DTO.Auth;
using HackathonWebsite.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers.AppliesController
{
    [ApiController]
    [Route("applies")]
    public class AppliesController(IApplyService applyService, IAuthService authService, IEmailSender emailSender) : ControllerBase
    {
        [HttpGet("hackaton-applies/get")]
        public async Task<IActionResult> GetHackApplies()
        {
            var role = authService.GetCurrentUserRoles();

            if (role != Roles.ADMIN) return Unauthorized("Not enough permissions");
            var applies = await applyService.GetApplyToHack();
            return Ok(applies);

        }

        [HttpPost("team-applies/send-invite/{applyId}")]
        public async Task<IActionResult> SendInvite(int applyId)
        {
            var apply = await applyService.GetApplyToTeamById(applyId);

            var team = apply.Team;
            var user = apply.User;
            var email = user.Email;
            var link = $"{Request.Scheme}://{Request.Host}/team/invite/{team.Link}";
            var canSend = await applyService.SendInvite(email, link, TeamMapper.TeamToDto(team));
            if (!canSend) throw new Exception("Сообщение не отправилось");
            return Ok(canSend);
        }

        [HttpPost("create-team-apply")]
        public async Task<IActionResult> CreateTeamApply(ApplyToTeamDto dto)
        {
            var currentId = authService.GetCurrentUserId();

            if (currentId != -1)
            {
                await applyService.CreateApplyTeam(dto);
                return Ok();
            }

            return Unauthorized("Not authorized");
        }

        [HttpPost("create-hack-apply")]
        public async Task<IActionResult> CreateHackatonApply(ApplyToHackDto dto)
        {
            var currentId = authService.GetCurrentUserId();

            if(currentId != -1)
            {
                await applyService.CreateApplyHack(dto);
                return Ok();
            }

            return Unauthorized("Not authorized");
        }

        [HttpPost("approve-hack-apply/{id}")]
        public async Task<IActionResult> ApproveHackatonApply(int id)
        {
            var role = authService.GetCurrentUserRoles();

            if (role == Roles.ADMIN)
            {
                await applyService.ApproveApplyHack(id);
                return Ok();
            }

            return Unauthorized("Not enough permissions");
        }
    }
}
