using HackathonWebsite.DataLayer.Repositories.Implementations;
using HackathonWebsite.DTO.Case;
using HackathonWebsite.DTO.Hackaton;

namespace HackathonWebsite.BusinessLayer.Services.CaseService
{
    public class CaseService(ICaseRepository caseRepository) : ICaseService
    {
        public async Task<int> Create(CaseDto @case)
        {
           return await caseRepository.Create(@case);
        }

        public Task<int> Delete(int id)
        {
            return caseRepository.Delete(id);
        }

        public async Task<CaseDto> GetById(int id)
        {
            return await caseRepository.GetById(id);
        }

        public async Task<CaseDto> GetByName(string name)
        {
            return await caseRepository.GetByName(name);
        }

        public async Task<int> Update(CaseDto @case)
        {
            return await caseRepository.Update(@case);
        }
    }
}
