using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO;

namespace HackathonWebsite.BusinessLayer.Services.ApplyService
{
    public interface IApplyService
    {
        Task<ICollection<ApplyToHackEntity>> GetApplyToHack();
        Task<ICollection<ApplyToTeamEntity>> GetApplyToTeam();


        Task<int> CreateApplyHack(ApplyToHackDto dto);
        Task ApproveApplyHack(int applyId);

        Task<int> CreateApplyTeam(ApplyToTeamDto dto);
        Task ApproveApplyTeam(int applyId);
    }
}
