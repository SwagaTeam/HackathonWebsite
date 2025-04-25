using HackathonWebsite.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public class TeamRepository(AppDbContext context) : ITeamRepository
    {
        public async Task<int> Create(TeamEntity team)
        {
            await context.Teams.AddAsync(team);
            await context.SaveChangesAsync();
            return team.Id;
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
            if (team is not null)
            {
                context.Teams.Update(team);
                await context.SaveChangesAsync();
            }
            else throw new NullReferenceException($"Не существует команды с айди {team.Id}");
        }

        public async Task<TeamEntity?> GetByLeadId(int id)
        {
            var team = await context.Teams.FirstOrDefaultAsync(x => x.LeaderId == id);
            if (team is not null)
                return team;
            throw new NullReferenceException($"Не существует команды с айди {team.Id}");
        }

        public async Task<TeamEntity?> GetByLink(string link)
        {
            var team = await context.Teams.FirstOrDefaultAsync(x => x.Link == link);
            if (team is not null)
                return team;
            throw new NullReferenceException($"Не существует команды с такой ссылкой");
        }

        public async Task AddInTeam(int teamId, int userId)
        {
            var team = await context.Teams.Include(x => x.Participants).FirstOrDefaultAsync(x=>x.Id == teamId);
            var user = await context.Users.FindAsync(userId);

            if (team is null || user is null) 
                throw new NullReferenceException($"Не существует команды с айди {team.Id}");

            team.Participants.Add(user);
            await context.SaveChangesAsync();
        }
    }
}
