using HackathonWebsite.DataLayer.Entities;

namespace HackathonWebsite.BusinessLayer.Services.UserService;

public interface IUserService
{
    public Task<UserEntity> GetByEmail(string email);
    public Task<UserEntity> GetById(int id);

}