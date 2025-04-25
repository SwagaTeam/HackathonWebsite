using HackathonWebsite.DataLayer.Repositories.Implementations;
using HackathonWebsite.DTO.Hackaton;
using HackathonWebsite.Mapper;

namespace HackathonWebsite.BusinessLayer.Services.HackathonService
{
    public class HackathonService(IHackathonRepository hackathonRepository) : IHackathonService
    {
        public async Task<int> Create(HackatonDto hackaton)
        {
            var hackatonEntity = HackatonMapper.HackathonToEntity(hackaton);
            await hackathonRepository.Create(hackatonEntity);
            return hackatonEntity.Id;
        }

        public async Task<int> Delete(int id)
        {
            await hackathonRepository.Delete(id);
            return id;
        }

        public async Task<HackatonDto> GetById(int id)
        {
            return HackatonMapper.HackathonToDto(await hackathonRepository.GetById(id));
        }

        public async Task<HackatonDto> GetByName(string name)
        {
            return HackatonMapper.HackathonToDto(await hackathonRepository.GetByName(name));
        }

        public async Task<int> Update(HackatonDto hackaton)
        {
            var hackatonEntity = HackatonMapper.HackathonToEntity(hackaton);
            await hackathonRepository.Update(hackatonEntity);
            return hackatonEntity.Id;
        }
    }
}
