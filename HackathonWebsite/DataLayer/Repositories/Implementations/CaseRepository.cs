using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO.Case;
using HackathonWebsite.DTO.Hackaton;
using HackathonWebsite.Mapper;
using Microsoft.EntityFrameworkCore;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public class CaseRepository(AppDbContext dbContext) : ICaseRepository
    {
        public async Task Create(CaseEntity @case)
        {
            await dbContext.Cases.AddAsync(@case);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existingCase = await GetById(id);

            if(existingCase is null)
                throw new NullReferenceException($"Не существует кейса Id {id}");

            dbContext.Remove(existingCase);
        }

        public async Task<CaseEntity> GetById(int id)
        {
            var existingCase = await dbContext.Cases.FindAsync(id);

            if (existingCase is null)
                throw new NullReferenceException($"Не существует кейса Id {id}");

            return existingCase;
        }

        public async Task<CaseEntity> GetByName(string name)
        {
            var existingCase = await dbContext.Cases.FirstOrDefaultAsync(x => x.Title == name);

            if (existingCase is null)
                throw new NullReferenceException($"Не существует кейса с названием {name}");

            return existingCase;
        }

        public async Task Update(CaseEntity @case)
        {
            if (@case is not null) dbContext.Cases.Update(@case);
            else throw new NullReferenceException($"Не существует кейса с айди {@case.Id}");
        }
    }
}
