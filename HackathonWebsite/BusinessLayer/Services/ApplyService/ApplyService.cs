using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.BusinessLayer.Services.TeamService;
using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DataLayer.Repositories.Implementations;
using HackathonWebsite.DTO;
using HackathonWebsite.DTO.Auth;

namespace HackathonWebsite.BusinessLayer.Services.ApplyService
{
    public class ApplyService(
        IApplyHackRepository applyHackRepo, 
        IApplyTeamRepository applyTeamRepo, 
        IAuthService authService, 
        ITeamService teamService) : IApplyService
    {
        public async Task ApproveApplyHack(int applyId)
        {
            var userRole = authService.GetCurrentUserRoles();

            if (userRole == Roles.ADMIN)
                await applyHackRepo.ApproveApply(applyId);
            else
                throw new Exception("Not enough permissions");
        }

        public Task ApproveApplyTeam(int applyId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateApplyHack(ApplyToHackDto dto)
        {
            var userId = authService.GetCurrentUserId();

            var team = await teamService.GetByLeadId((int)userId!);

            var entity = new ApplyToHackEntity()
            {
                Description = dto.Description,
                CaseId = team.CaseId,
                TeamId = team.Id,
                IsApplied = false,
            };

            return await applyHackRepo.CreateApply(entity);
        }

        public Task<int> CreateApplyTeam(ApplyToTeamDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ApplyToHackEntity>> GetApplyToHack()
        {
            return await applyHackRepo.Get();
        }

        public Task<ICollection<ApplyToTeamEntity>> GetApplyToTeam()
        {
            throw new NotImplementedException();
        }
    }
}
