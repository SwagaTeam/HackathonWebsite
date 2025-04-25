using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public interface IApplyHackRepository
    {
        Task<ICollection<ApplyToHackEntity>> Get();
        Task<int> CreateApply(ApplyToHackEntity entity);
        Task ApproveApply(int applyId);
    }
}
