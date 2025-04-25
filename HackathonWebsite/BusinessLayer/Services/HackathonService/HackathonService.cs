using HackathonWebsite.DataLayer.Repositories.Implementations;
using HackathonWebsite.DTO.Hackaton;

namespace HackathonWebsite.BusinessLayer.Services.HackathonService
{
    public class HackathonService(IHackathonRepository hackathonRepository) : IHackathonService
    {
        public async Task<int> Create(HackatonDto hackaton)
        {
            return await hackathonRepository.Create(hackaton);
        }

        public async Task<int> Delete(int id)
        {
            return await hackathonRepository.Delete(id);
        }

        public async Task<HackatonDto> GetById(int id)
        {
            return await hackathonRepository.GetById(id);
        }

        public async Task<HackatonDto> GetByName(string name)
        {
            return await hackathonRepository.GetByName(name);
        }

        public async Task<int> Update(HackatonDto hackaton)
        {
            return await hackathonRepository.Update(hackaton);
        }
    }
}
