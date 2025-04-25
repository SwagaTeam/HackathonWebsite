using HackathonWebsite.DTO.Hackaton;
using HackathonWebsite.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using HackathonWebsite.DataLayer.Entities;
using Org.BouncyCastle.Pqc.Crypto.Falcon;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public class HackathonRepository(AppDbContext dbContext) : IHackathonRepository
    {
        public async Task Create(HackathonEntity hackaton)
        {
            await dbContext.AddAsync(hackaton);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var hackaton = await GetById(id);
            if (hackaton is not null)
            {
                dbContext.Hackathons.Remove(hackaton);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<HackathonEntity> GetActiveHackaton()
        {
            var hackaton = await dbContext.Hackathons.FirstOrDefaultAsync(x=>x.IsActive);

            if (hackaton is null)
                throw new NullReferenceException($"Не существует хакатона с айди {hackaton.Id}");

            return hackaton;
        }

        public async Task<HackathonEntity> GetById(int id)
        {
            var hackaton = await dbContext.Hackathons.FindAsync(id);
            return hackaton;
        }

        public async Task<HackathonEntity> GetByName(string name)
        {
            var hackaton = await dbContext.Hackathons.FirstOrDefaultAsync(x=>x.Title == name);
            return hackaton;
        }

        public async Task<int> SetActiveHackaton(int id)
        {
            var hackaton = await dbContext.Hackathons.FindAsync(id);

            if (hackaton is null)
                throw new NullReferenceException($"Не существует хакатона с айди {hackaton.Id}");

            await dbContext.Hackathons.
                    ExecuteUpdateAsync(h => h
                    .SetProperty(h => h.IsActive, false));

            hackaton.IsActive = true;

            await dbContext.SaveChangesAsync();

            return id;
        }

        public async Task Update(HackathonEntity hackaton)
        {
            if (hackaton is not null) dbContext.Hackathons.Update(hackaton);
            else throw new NullReferenceException($"Не существует хакатона с айди {hackaton.Id}");
        }
    }
}
