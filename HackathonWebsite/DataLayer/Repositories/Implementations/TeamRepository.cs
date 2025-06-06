﻿using HackathonWebsite.DataLayer.Entities;
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
            if (team is null) throw new NullReferenceException("Команда не найдена");
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

        public async Task<TeamEntity?> GetByLink(string link)
        {
            var team = await context.Teams.Include(x => x.Participants).FirstOrDefaultAsync(x => x.Link == link);
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

        public async Task AddGithubLink(int teamId, string link)
        {
            var team = await context.Teams.FirstOrDefaultAsync(x => x.Id == teamId);
            if (team is not null) 
            {
                team.GitHubLink = link;
                await context.SaveChangesAsync();
                return;
            }
                
            throw new NullReferenceException($"Не существует команды с айди {team.Id}");
        }

        public async Task AddGoogleLink(int teamId, string link)
        {
            var team = await context.Teams.FirstOrDefaultAsync(x => x.Id == teamId);
            if (team is not null)
            {
                team.GoogleDiskLink = link;
                await context.SaveChangesAsync();
                return;
            }

            throw new NullReferenceException($"Не существует команды с айди {team.Id}");
        }
        
        
        public async Task<TeamEntity?> GetByLeadId(int id)
        {
            var team = await context.Teams.Include(x=>x.Participants).FirstOrDefaultAsync(x => x.LeaderId == id);
            if (team is not null)
                return team;
            return null;
        }

        public async Task<ICollection<TeamEntity>> Get()
        {
            return await context.Teams.ToListAsync();
        }
        
        public async Task<TeamEntity?> GetByUserId(int id)
        {
            var team = await context.Teams
                .Include(x=>x.Participants)
                .Where(x=>x.Participants.Any(x=>x.Id == id))
                .FirstOrDefaultAsync();

            if (team is not null)
                return team;
            throw new NullReferenceException($"Не существует команды с юзером {team.Id}");
        }
    }
}
