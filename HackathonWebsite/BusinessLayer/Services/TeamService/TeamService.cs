using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.BusinessLayer.Services.CaseService;
using HackathonWebsite.DataLayer.Repositories.Implementations;
using HackathonWebsite.Dto.Team;
using HackathonWebsite.Mapper;

namespace HackathonWebsite.BusinessLayer.Services.TeamService
{
    public class TeamService(ITeamRepository repository, ICaseService caseService, IAuthService authService) : ITeamService
    {
        public async Task<int> Create(TeamDto team)
        {
            var @case = await caseService.GetById(team.CaseId);
            if (@case is null) throw new NullReferenceException("Нельзя создать команду под несуществующий кейс");
            var currentId = authService.GetCurrentUserId();
            team.LeaderId = currentId;
            var teamEntity = TeamMapper.TeamToEntity(team);
            await repository.Create(teamEntity);
            return team.Id;
        }

        public async Task<int> Update(TeamDto team)
        {
            await repository.Update(TeamMapper.TeamToEntity(team));
            return team.Id;
        }

        public async Task SendInvite()
        {
            throw new NotImplementedException();
        }

        public async Task CheckInvites()
        {
            throw new NotImplementedException();
        }
    }
}
