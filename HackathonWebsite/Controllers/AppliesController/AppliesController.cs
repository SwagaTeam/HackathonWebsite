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
            try
            {
                var role = authService.GetCurrentUserRoles();

                if (role != Roles.ADMIN) return Unauthorized("Not enough permissions");
                var applies = await applyService.GetApplyToHack();
                return Ok(applies);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("team-applies/get")]
        public async Task<IActionResult> GetTeamApplies()
        {
            try
            {
                var applies = await applyService.GetApplyToTeam();
                return Ok(applies);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("team-applies/send-invite/{applyId}")]
        public async Task<IActionResult> SendInvite(int applyId)
        {
            try
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
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-team-apply")]
        public async Task<IActionResult> CreateTeamApply(ApplyToTeamDto dto)
        {
            try
            {
                var currentId = authService.GetCurrentUserId();

                if (currentId != -1)
                {
                    await applyService.CreateApplyTeam(dto);
                    return Ok();
                }

                return Unauthorized("Not authorized");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-hack-apply")]
        public async Task<IActionResult> CreateHackatonApply(ApplyToHackDto dto)
        {
            try
            {
                var currentId = authService.GetCurrentUserId();

                if (currentId != -1)
                {
                    await applyService.CreateApplyHack(dto);
                    return Ok();
                }

                return Unauthorized("Not authorized");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("approve-hack-apply/{id}")]
        public async Task<IActionResult> ApproveHackatonApply(int id)
        {
            try
            {
                var role = authService.GetCurrentUserRoles();

                if (role == Roles.ADMIN)
                {
                    await applyService.ApproveApplyHack(id);
                    return Ok();
                }

                return Unauthorized("Not enough permissions");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
