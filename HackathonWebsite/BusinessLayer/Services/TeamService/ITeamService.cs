using HackathonWebsite.DataLayer.Entities;

namespace HackathonWebsite.BusinessLayer.Services.TeamService
{
    public interface ITeamService
    {
        public Task Create(TeamEntity team);
        public Task Update(TeamEntity team);
        public Task<TeamEntity?> GetById(int id);
        public Task Delete(TeamEntity team);
    }
}
