using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO.Hackaton;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public interface IHackathonRepository
    {
        Task Create(HackathonEntity hackaton);
        Task Update(HackathonEntity hackaton);
        Task Delete(int id);
        Task<HackathonEntity> GetById(int id);
        Task<HackathonEntity> GetByName(string name);
    }
}
