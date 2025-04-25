using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO;
using HackathonWebsite.Dto.Team;

namespace HackathonWebsite.BusinessLayer.Services.ApplyService
{
    public interface IApplyService
    {
        Task<ICollection<ApplyToHackEntity>> GetApplyToHack();
        Task<ICollection<ApplyToTeamEntity>> GetApplyToTeam();


        Task<int> CreateApplyHack(ApplyToHackDto dto);
        Task ApproveApplyHack(int applyId);

        Task<int> CreateApplyTeam(ApplyToTeamDto dto);
        Task<bool> SendInvite(string email, string link, TeamDto team);
        Task<ApplyToTeamEntity> GetApplyToTeamById(int applyId);
    }
}
