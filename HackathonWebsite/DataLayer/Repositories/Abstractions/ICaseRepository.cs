using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO.Case;
using HackathonWebsite.DTO.Hackaton;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public interface ICaseRepository
    {
        Task Create(CaseEntity @case);
        Task Update(CaseEntity @case);
        Task<ICollection<CaseEntity>> Get();
        Task Delete(int id);
        Task<CaseEntity> GetById(int id);
        Task<CaseEntity> GetByName(string name);
    }
}
