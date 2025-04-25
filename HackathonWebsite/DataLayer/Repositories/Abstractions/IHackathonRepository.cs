using HackathonWebsite.DTO.Hackaton;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public interface IHackathonRepository
    {
        Task<int> Create(HackatonDto hackaton);
        Task<int> Update(HackatonDto hackaton);
        Task<int> Delete(int id);
        Task<HackatonDto> GetById(int id);
        Task<HackatonDto> GetByName(string name);
    }
}
