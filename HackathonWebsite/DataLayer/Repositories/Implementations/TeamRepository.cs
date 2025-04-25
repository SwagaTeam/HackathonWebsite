using HackathonWebsite.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public class TeamRepository(AppDbContext context) : ITeamRepository
    {
        public async Task Create(TeamEntity team)
        {
            await context
                .Teams
                .AddAsync(team);
            await context
                .SaveChangesAsync();
        }

        public async Task<TeamEntity> GetById(int id)
        {
            return await context
                .Teams
                .FindAsync(id);
        }

        public async Task Delete(int id)
        {
            var team = await GetById(id);
            if (team is null) throw new NullReferenceException();
            context
                .Teams
                .Remove(team);
            await context
                .SaveChangesAsync();
        }

        public async Task Update(TeamEntity team)
        {
            if (team is not null) context.Teams.Update(team);
            else throw new NullReferenceException($"Не существует хакатона с айди {team.Id}");
        }
    }
}
