using HackathonWebsite.DataLayer.Entities;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public interface IApplyTeamRepository
    {
        Task<ICollection<ApplyToTeamEntity>> Get();
        Task<int> CreateApply(ApplyToTeamEntity entity);
        Task<ApplyToTeamEntity> GetById(int id);
        Task Apply(int applyId);
    }
}
