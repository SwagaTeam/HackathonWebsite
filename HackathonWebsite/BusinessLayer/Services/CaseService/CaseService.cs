using HackathonWebsite.BusinessLayer.Services.HackathonService;
using HackathonWebsite.DataLayer.Repositories.Implementations;
using HackathonWebsite.DTO.Case;
using HackathonWebsite.DTO.Hackaton;
using HackathonWebsite.Mapper;

namespace HackathonWebsite.BusinessLayer.Services.CaseService
{
    public class CaseService(ICaseRepository caseRepository, IHackathonService hackatonService) : ICaseService
    {
        public async Task<int> Create(CaseDto @case)
        {
            var existingHackaton = await hackatonService.GetById(@case.HackathonId);
            if (existingHackaton is null) 
                throw new NullReferenceException($"Не существует хакатона с Id {@case.HackathonId}");
            var caseEntity = CaseMapper.CaseToEntity(@case);

            await caseRepository.Create(caseEntity);
            return @case.Id;
        }

        public async Task<int> Delete(int id)
        {
            await caseRepository.Delete(id);
            return id;
        }

        public async Task<CaseDto> GetById(int id)
        {
            return CaseMapper.CaseToDto(await caseRepository.GetById(id));
        }

        public async Task<CaseDto> GetByName(string name)
        {
            return CaseMapper.CaseToDto(await caseRepository.GetByName(name));
        }

        public async Task<int> Update(CaseDto @case)
        {
            await caseRepository.Update(CaseMapper.CaseToEntity(@case));
            return @case.Id;
        }
    }
}
