using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO.Hackaton;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public interface IHackathonRepository
    {
        Task Create(HackathonEntity hackaton);
        Task Update(HackathonEntity hackaton);
        Task<ICollection<HackathonEntity>> Get();
        Task Delete(int id);
        Task<HackathonEntity> GetById(int id);
        Task<HackathonEntity> GetByName(string name);
        Task<HackathonEntity> GetActiveHackaton();
        Task<int> SetActiveHackaton(int id);
    }
}
