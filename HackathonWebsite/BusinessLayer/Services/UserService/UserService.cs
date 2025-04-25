using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.BusinessLayer.Services.TeamService;
using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DataLayer.Repositories.Implementations;

namespace HackathonWebsite.BusinessLayer.Services.UserService;

public class UserService(IUserRepository repository, IAuthService authService, ITeamRepository teamRepository) : IUserService
{
    public async Task<UserEntity> GetByEmail(string email)
    {
        return await repository.GetByEmail(email);
    }

    public async Task<UserEntity> GetById(int id)
    {
        return await repository.GetById(id);
    }

    public async Task<int> SetRole(int id, string role)
    {
        var currentUserId = authService.GetCurrentUserId();

        if (currentUserId == -1)
            throw new Exception("Not authorized");

        var team = await teamRepository.GetByLeadId(id);

        var user = await GetById((int)currentUserId);

        if (team.Participants.Contains(user))
            return await repository.SetRole(id, role);
        else
            throw new Exception("User is not in your team, set role can only Leads");
    }
}