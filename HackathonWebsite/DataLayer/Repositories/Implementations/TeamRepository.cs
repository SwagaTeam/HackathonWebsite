using HackathonWebsite.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public class TeamRepository(AppDbContext context) : ITeamRepository
    {
        public async Task Create(TeamEntity team)
        {
            context.Teams.AddAsync(team);
            await context.SaveChangesAsync();
        }

        public async Task Update(TeamEntity team)
        {
           context.Teams.Update(team);
           await context.SaveChangesAsync();
        }

        public async Task<TeamEntity?> GetById(int id)
        {
            return await context.Teams.FindAsync(id);
        }

        public async Task Delete(TeamEntity team)
        {
            context.Teams.Remove(team);
            await context.SaveChangesAsync();
        }
    }
}
