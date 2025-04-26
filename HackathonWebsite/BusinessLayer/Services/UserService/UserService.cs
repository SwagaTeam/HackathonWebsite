using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.BusinessLayer.Services.TeamService;
using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DataLayer.Repositories.Implementations;
using HackathonWebsite.DTO.Auth;

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

    //оч странная штука, изначально роли были чтобы отделить юзера от админа, если менять роль в зависимости от команды,
    //то нужно это прорабатывать у юзера, если он выходит из тимы то удалять роль.
    public async Task<int> SetRole(int id, string role)
    {
        var currentUserId = authService.GetCurrentUserId();

        if (currentUserId == -1)
            throw new Exception("Not authorized");

        if (role == Roles.ADMIN) throw new Exception("Нельзя сделать пользователя админом");

        var team = await teamRepository.GetByLeadId(id);

        var user = await GetById((int)currentUserId);

        if (team.Participants.Contains(user))
            return await repository.SetRole(id, role);
        else
            throw new Exception("User is not in your team, set role can only Leads");
    }
}