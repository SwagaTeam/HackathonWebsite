using HackathonWebsite.BusinessLayer.Services.AuthService.Implementations;
using HackathonWebsite.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace HackathonWebsite.Initializers
{
    public class DbContextInitializer
    {
        public static void Initialize(IServiceCollection services, string conn)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(conn));
        }

        public static async Task Migrate(AppDbContext context)
        {
            var existingAdmin = await context.Admins.FirstOrDefaultAsync();

            if (existingAdmin is null)
            {
                string salt = Guid.NewGuid().ToString();

                var encrypt = new Encrypt();

                await context.Admins.AddAsync(new DataLayer.Entities.AdminEntity()
                {
                    Nickname = "admin",
                    Password = encrypt.HashPassword("admin", salt),
                    Salt = salt
                });
            }

            await context.Database.MigrateAsync();
            await context.SaveChangesAsync();
        }
    }
}
