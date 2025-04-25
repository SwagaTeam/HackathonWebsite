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
            return BadRequest("������ ������������ �� ���������� ���� ��� �������");

        await mailSender.SendEmailAsync(
            email,
            "����������� � �������",
            $"������������, {user.FullName}, �� ���� ���������� �� ������� � ������� {team.Title}.\n" +
            $"������-�����������: {link}");

        return Ok(link);
    }

    [HttpPost("invite/{link}")]
    public async Task<IActionResult> AcceptInvite(string link)
    {
        var existingTeam = await teamService.GetByLink(link);

        var currentUserId = authService.GetCurrentUserId();

        if (currentUserId < 0)
            return Unauthorized("�� �� ������������");

        await teamService.AddInTeam(existingTeam.Id, (int)currentUserId);

        return Ok($"�� ��������� � ������� {existingTeam.Title}!");
    }
}