using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.BusinessLayer.Services.CaseService;
using HackathonWebsite.BusinessLayer.Services.UserService;
using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DataLayer.Repositories.Implementations;
using HackathonWebsite.Dto.Team;
using HackathonWebsite.Mapper;

namespace HackathonWebsite.BusinessLayer.Services.TeamService
{
    public class TeamService(
        ITeamRepository repository, 
        ICaseService caseService, 
        IAuthService authService, 
        IUserService userService) : ITeamService
    {
        public async Task<int> Create(TeamDto team)
        {
            //var @case = await caseService.GetById(team.CaseId);
            //Андрей сказал можно 
            //if (@case is null) throw new NullReferenceException("Нельзя создать команду под несуществующий кейс");
            var currentId = authService.GetCurrentUserId();
            team.LeaderId = currentId;
            team.Link = Guid.NewGuid().ToString();
            var teamEntity = TeamMapper.TeamToEntity(team);
            var id = await repository.Create(teamEntity);

            await AddInTeam(id, (int)currentId!);

            return id;
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

        public async Task<TeamDto> GetByLeadId(int id)
        {
            var team = await repository.GetByLeadId(id);
            return TeamMapper.TeamToDto(team);
        }

        public async Task<TeamDto> GetByLink(string link)
        {
            var team = await repository.GetByLink(link);
            return TeamMapper.TeamToDto(team);
        }

        public async Task AddInTeam(int teamId, int userId)
        {
            await repository.AddInTeam(teamId, userId);
        }

        public async Task AddGithubLink(int teamId, string link)
        {
            await repository.AddGithubLink(teamId, link);
        }

        public async Task AddGoogleLink(int teamId, string link)
        {
            await repository.AddGoogleLink(teamId, link);
        }
        

        public async Task<ICollection<TeamEntity>> Get()
        {
            var teams = await repository.Get();
            return teams;
        }

        public async Task<TeamEntity> GetById(int id)
        {
            var team = await repository.GetById(id);
            return team;
        }
    }
}
