using HackathonWebsite.DTO.Hackaton;

namespace HackathonWebsite.BusinessLayer.Services.HackathonService
{
    public interface IHackathonService
    {
        Task<int> Create(HackatonDto hackaton);
        Task<int> Update(HackatonDto hackaton);
        Task<int> Delete(HackatonDto hackaton);
        Task<HackatonDto> GetById(int id);
        Task<HackatonDto> GetByName(string name);
    }
}
