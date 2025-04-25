using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.Dto.Team;

namespace HackathonWebsite.BusinessLayer.Services.TeamService
{
    public interface ITeamService
    {
        public Task<int> Create(TeamDto team);

        public Task<TeamDto> GetByLeadId(int id);
        public Task<TeamDto> GetByLink(string link);
        public Task<int> Update(TeamDto team);
        public Task SendInvite();
        public Task CheckInvites();
        public Task AddInTeam(int teamId, int userId);
    }
}
