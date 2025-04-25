using HackathonWebsite.BusinessLayer.Services.AuthService.Abstractions;
using HackathonWebsite.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    internal class UserRepository(AppDbContext context, IEncrypt encrypt) : IUserRepository
    {
        public async Task Create(UserEntity user)
        {
            await context
                .Users
                .AddAsync(user);
            await context
                .SaveChangesAsync();
        }

        public async Task<UserEntity?> GetById(int id)
        {
            return await context
                .Users
                .FindAsync(id);
        }

        public async Task<UserEntity?> GetByEmail(string email)
        {
            return await context
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task Delete(int id)
        {
            var user = await GetById(id);
            if (user is null) throw new NullReferenceException();
            context
                .Users
                .Remove(user);
            await context
                .SaveChangesAsync();
        }

        public async Task Update(UserEntity user)
        {
            context
                .Users
                .Update(user);
            await context
                .SaveChangesAsync();
        }
    }
}
