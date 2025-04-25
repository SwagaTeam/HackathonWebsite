using HackathonWebsite.DTO.Case;
using HackathonWebsite.DTO.Hackaton;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public interface ICaseRepository
    {
        Task<int> Create(CaseDto @case);
        Task<int> Update(CaseDto @case);
        Task<int> Delete(int id);
        Task<CaseDto> GetById(int id);
        Task<CaseDto> GetByName(string name);
    }
}
