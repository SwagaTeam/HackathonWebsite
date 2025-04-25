using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.Dto.Team;

namespace HackathonWebsite.BusinessLayer.Services.TeamService
{
    public interface ITeamService
    {
        public Task<int> Create(TeamDto team);
        public Task<int> Update(TeamDto team);
        public Task SendInvite();
        public Task CheckInvites();
    }
}
