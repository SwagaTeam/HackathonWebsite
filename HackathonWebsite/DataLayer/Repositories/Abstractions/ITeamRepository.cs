using HackathonWebsite.DataLayer.Entities;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public interface ITeamRepository
    {
        public Task<int> Create(TeamEntity team);
        public Task Update(TeamEntity team);
        public Task<TeamEntity?> GetById(int id);
        public Task<TeamEntity?> GetByLeadId(int id);
        public Task<TeamEntity?> GetByLink(string link);
        public Task AddInTeam(int teamId, int userId);
        public Task Delete(int id);
    }
}
