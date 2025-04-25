using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DataLayer.Repositories.Implementations;

namespace HackathonWebsite.BusinessLayer.Services.UserService;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task<UserEntity> GetByEmail(string email)
    {
        return await repository.GetByEmail(email);
    }
}