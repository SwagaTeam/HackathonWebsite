using HackathonWebsite.BusinessLayer.Services.TeamService;
using HackathonWebsite.Dto.Team;
using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers.Team;

[ApiController]
[Route("team")]
public class TeamController(ITeamService teamService) : ControllerBase
{
    [HttpPost]
    public async Task<int> Create([FromBody] TeamDto team)
    {
        return await teamService.Create(team);
    }
}