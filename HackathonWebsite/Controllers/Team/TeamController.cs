using HackathonWebsite.BusinessLayer.Services.ApplyService;
using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.BusinessLayer.Services.MailService;
using HackathonWebsite.BusinessLayer.Services.TeamService;
using HackathonWebsite.BusinessLayer.Services.UserService;
using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DataLayer.Repositories.Implementations;
using HackathonWebsite.Dto.Team;
using HackathonWebsite.DTO.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers.Team;

[ApiController]
[Route("team")]
public class TeamController(
    ITeamService teamService, 
    IAuthService authService,
    IUserService userService,
    IApplyService applyService) : ControllerBase
{
    [HttpPost]
    public async Task<int> Create([FromBody] TeamDto team)
    {
        return await teamService.Create(team);
    }

    [HttpPost("send-invite")]
    public async Task<IActionResult> SendInvite(string email)
    {
        try
        {
            var currentUserId = authService.GetCurrentUserId();
            var team = await teamService.GetByLeadId((int)currentUserId!);
            if (team is null) throw new Exception("Не лидер команды не может приглашать участников в нее");
            var link = $"{Request.Scheme}://{Request.Host}/team/invite/{team.Link}";
            var canSend = await applyService.SendInvite(email, link, team);
            if (canSend) return Ok(link);
            return BadRequest("Не получилось отправить письмо((");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("invite/{link}")]
    public async Task<IActionResult> ShowInvite(string link)
    {
        try
        {
            var team = await teamService.GetByLink(link);
            if (team == null) return NotFound();

            return Ok("Вступление в команду");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("invite/{link}")]
    public async Task<IActionResult> AcceptInvite(string link)
    {
        try
        {
            var existingTeam = await teamService.GetByLink(link);

            var currentUserId = authService.GetCurrentUserId();

            if (currentUserId < 0)
                return Unauthorized("Вы не авторизованы");

            await teamService.AddInTeam(existingTeam.Id, (int)currentUserId);

            return Ok($"Вы добавлены в команду {existingTeam.Title}!");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPost("add-github")]
    public async Task<IActionResult> AddGithubLink([FromBody]string link)
    {
        try
        {
            var currentUserId = authService.GetCurrentUserId();

            if (currentUserId == -1)
                return Unauthorized("Not authorized");

            var team = await teamService.GetByUser((int)currentUserId!);

            await teamService.AddGithubLink(team.Id, link);

            return Ok($"Ссылка добавлена");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPost("add-google")]
    public async Task<IActionResult> AddGoogleLink(string link)
    {
        try
        {
            var currentUserId = authService.GetCurrentUserId();

            if (currentUserId == -1)
                return Unauthorized("Not authorized");

            var team = await teamService.GetByUser((int)currentUserId!);

            await teamService.AddGoogleLink(team.Id, link);

            return Ok($"Ссылка добавлена");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpGet("my-team")]
    public async Task<IActionResult> GetCurrentTeam()
    {
        try
        {
            var currentUserId = authService.GetCurrentUserId();

            TeamDto team;

            if (currentUserId == -1)
                return Unauthorized();
            try
            {
                team = await teamService.GetByUser((int)currentUserId!);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(team);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var role = authService.GetCurrentUserRoles();
            TeamEntity team;
            if (role != Roles.ADMIN)
                return Unauthorized("Not enough permissions");
            try
            {
                team = await teamService.GetById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(team);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var role = authService.GetCurrentUserRoles();

            if (role != Roles.ADMIN)
                return Unauthorized("Not enough permissions");

            var team = await teamService.Get();

            return Ok(team);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}