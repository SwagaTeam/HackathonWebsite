using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO.Case;
using HackathonWebsite.DTO.Hackaton;

namespace HackathonWebsite.BusinessLayer.Services.CaseService
{
    public interface ICaseService
    {
        Task<int> Create(CaseDto @case);
        Task<int> Update(CaseDto @case);
        Task<ICollection<CaseEntity>> Get();
        Task<int> Delete(int id);
        Task<CaseDto> GetById(int id);
        Task<CaseDto> GetByName(string name);
    }
}
