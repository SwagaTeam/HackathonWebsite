using HackathonWebsite.DataLayer.Entities;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public interface IUserRepository
    {
        Task Create(UserEntity user);
        Task<UserEntity> GetById(int id);
        Task<UserEntity> GetByEmail(string email);
        Task Delete(int id);
        Task Update(UserEntity user);
        Task<int> SetRole(int id, string role);
    }
}