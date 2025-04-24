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
            await context.Database.MigrateAsync();
            await context.SaveChangesAsync();
        }
    }
}
