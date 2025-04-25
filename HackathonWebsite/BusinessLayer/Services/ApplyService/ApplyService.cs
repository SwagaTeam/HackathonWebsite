using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.BusinessLayer.Services.MailService;
using HackathonWebsite.BusinessLayer.Services.TeamService;
using HackathonWebsite.BusinessLayer.Services.UserService;
using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DataLayer.Repositories.Implementations;
using HackathonWebsite.DTO;
using HackathonWebsite.DTO.Auth;
using HackathonWebsite.Dto.Team;

namespace HackathonWebsite.BusinessLayer.Services.ApplyService
{
    public class ApplyService(
        IApplyHackRepository applyHackRepo, 
        IApplyTeamRepository applyTeamRepo, 
        IAuthService authService, 
        ITeamService teamService,
        IUserService userService,
        IEmailSender mailSender) : IApplyService
    {
        public async Task ApproveApplyHack(int applyId)
        {
            var userRole = authService.GetCurrentUserRoles();

            if (userRole == Roles.ADMIN)
                await applyHackRepo.ApproveApply(applyId);
            else
                throw new Exception("Not enough permissions");
        }
        
        public async Task<int> CreateApplyHack(ApplyToHackDto dto)
        {
            var userId = authService.GetCurrentUserId();

            var team = await teamService.GetByLeadId((int)userId!);

            var entity = new ApplyToHackEntity
            {
                Description = dto.Description,
                CaseId = team.CaseId,
                TeamId = team.Id,
                IsApplied = false,
            };

            return await applyHackRepo.CreateApply(entity);
        }

        public async Task<int> CreateApplyTeam(ApplyToTeamDto dto)
        {
            var userId = authService.GetCurrentUserId();
            var entity = new ApplyToTeamEntity
            {
                Description = dto.Description,
                UserId = userId,
                IsApplied = false
            };
            
            return await applyTeamRepo.CreateApply(entity);
        }

        public async Task<ICollection<ApplyToHackEntity>> GetApplyToHack()
        {
            return await applyHackRepo.Get();
        }

        public async Task<ICollection<ApplyToTeamEntity>> GetApplyToTeam()
        {
            return await applyTeamRepo.Get();
        }

        public async Task<bool> SendInvite(string email, string link, TeamDto team)
        {
            var user = await userService.GetByEmail(email);
            if (user is null) throw new Exception("Такого пользователя не существует");
            return await mailSender.SendEmailAsync(email, "Приглашение в команду",
                $"Здравствуйте, {user.FullName}, вы были приглашены на хакатон в команду {team.Title}.\n" +
                $"Ссылка-приглашение: {link}");
        }

        public async Task<ApplyToTeamEntity> GetApplyToTeamById(int applyId)
        {
            return await applyTeamRepo.GetById(applyId);
        }
    }
}
