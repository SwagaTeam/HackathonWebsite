using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.Dto.Team;

namespace HackathonWebsite.BusinessLayer.Services.TeamService
{
    public interface ITeamService
    {
        public Task<int> Create(TeamDto team);
        public Task<ICollection<TeamEntity>> Get();
        public Task<TeamEntity> GetById(int id);
        public Task<TeamDto> GetByLeadId(int id);
        public Task<TeamDto> GetByLink(string link);
        public Task<int> Update(TeamDto team);
        public Task SendInvite();
        public Task CheckInvites();
        public Task AddInTeam(int teamId, int userId);
        public Task AddGithubLink(int teamId, string link);
        public Task AddGoogleLink(int teamId, string link);
        Task<TeamDto> GetByUser(int id);
    }
}
