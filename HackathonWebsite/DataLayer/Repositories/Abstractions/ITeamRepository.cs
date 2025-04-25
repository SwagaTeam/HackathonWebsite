using HackathonWebsite.DataLayer.Entities;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public interface ITeamRepository
    {
        public Task Create(TeamEntity team);
        public Task Update(TeamEntity team);
        public Task<TeamEntity?> GetById(int id);
        public Task Delete(int id);
    }
}
