using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public class AdminRepository(AppDbContext context) : IAdminRepository
    {
        public async Task Create(AdminEntity admin)
        {
            await context
                .Admins
                .AddAsync(admin);
            await context
                .SaveChangesAsync();
        }

        public async Task<AdminEntity?> GetById(int id)
        {
            return await context
                .Admins
                .FindAsync(id);
        }

        public async Task<AdminEntity> GetByEmail(string email)
        {
            return await context
                .Admins
                .FirstOrDefaultAsync(u => u.Nickname == email);
        }

        public async Task Delete(int id)
        {
            var admin = await GetById(id);
            if (admin is null) throw new NullReferenceException();
            context
                .Admins
                .Remove(admin);
            await context
                .SaveChangesAsync();
        }

        public async Task Update(AdminEntity admin)
        {
            context
                .Admins
                .Update(admin);
            await context
                .SaveChangesAsync();
        }
    }
}
