using HackathonWebsite.DataLayer.Entities;

namespace HackathonWebsite.DataLayer.Repositories.Abstractions
{
    public interface IAdminRepository
    {
        public Task Create(AdminEntity admin);
        public Task Update(AdminEntity admin);
        public Task Delete(int id);
        public Task<AdminEntity> GetById(int id);
        public Task<AdminEntity> GetByEmail(string email);
    }
}
