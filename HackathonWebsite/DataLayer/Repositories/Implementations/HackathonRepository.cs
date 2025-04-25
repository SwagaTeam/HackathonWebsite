using HackathonWebsite.DTO.Hackaton;
using HackathonWebsite.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public class HackathonRepository(AppDbContext dbContext) : IHackathonRepository
    {
        public async Task<int> Create(HackatonDto hackaton)
        {
            await dbContext.Hackathons.AddAsync(HackatonMapper.HackathonDtoToEntity(hackaton));
            await dbContext.SaveChangesAsync();

            return hackaton.Id;
        }

        public async Task<int> Delete(int id)
        {
            var hackaton = await dbContext.Hackathons.FindAsync(id);
            if (hackaton is not null)
            {
                dbContext.Hackathons.Remove(hackaton);
                await dbContext.SaveChangesAsync();
            }
            return id;
        }

        public async Task<HackatonDto> GetById(int id)
        {
            var hackaton = await dbContext.Hackathons.FindAsync(id);
            return HackatonMapper.HackathonEntityToDto(hackaton);
        }

        public async Task<HackatonDto> GetByName(string name)
        {
            var hackaton = await dbContext.Hackathons.FirstOrDefaultAsync(x=>x.Title == name);
            return HackatonMapper.HackathonEntityToDto(hackaton);
        }

        public async Task<int> Update(HackatonDto hackaton)
        {
            var entity = await dbContext.Hackathons.FirstOrDefaultAsync(x => x.Title == hackaton.Title);

            if (entity is not null)
            {
                entity.Title = hackaton.Title;
                entity.Description = hackaton.Description;
                await dbContext.SaveChangesAsync();
            }
            else
                throw new NullReferenceException($"Не существует хакатона с айди {hackaton.Id}");

            return hackaton.Id;

        }
    }
}
