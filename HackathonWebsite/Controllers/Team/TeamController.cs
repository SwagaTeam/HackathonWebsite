using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.BusinessLayer.Services.MailService;
using HackathonWebsite.BusinessLayer.Services.TeamService;
using HackathonWebsite.BusinessLayer.Services.UserService;
using HackathonWebsite.DataLayer.Repositories.Implementations;
using HackathonWebsite.Dto.Team;
using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers.Team;

[ApiController]
[Route("team")]
public class TeamController(
    ITeamService teamService, 
    IAuthService authService,
    IUserService userService,
    IEmailSender mailSender) : ControllerBase
{
    [HttpPost]
    public async Task<int> Create([FromBody] TeamDto team)
    {
        return await teamService.Create(team);
    }

    [HttpPost("send-invite")]
    public async Task<IActionResult> SendInvite(string email)
    {
        var currentUserId = authService.GetCurrentUserId();
        var team = await teamService.GetByLeadId((int)currentUserId!);
        var link = team.Link;

        link = $"{Request.Scheme}://{Request.Host}/team/invite/{link}";

        var user = await userService.GetByEmail(email);

        if (user is null || team is null)
            return BadRequest("Такого пользователя не существует либо нет команды");

        await mailSender.SendEmailAsync(
            email,
            "Приглашение в команду",
            $"Здравствуйте, {user.FullName}, вы были приглашены на хакатон в команду {team.Title}.\n" +
            $"Ссылка-приглашение: {link}");

        return Ok(link);
    }

    [HttpPost("invite/{link}")]
    public async Task<IActionResult> AcceptInvite(string link)
    {
        var existingTeam = await teamService.GetByLink(link);

        var currentUserId = authService.GetCurrentUserId();

        if (currentUserId < 0)
            return Unauthorized("Вы не авторизованы");

        await teamService.AddInTeam(existingTeam.Id, (int)currentUserId);

        return Ok($"Вы добавлены в команду {existingTeam.Title}!");
    }
}