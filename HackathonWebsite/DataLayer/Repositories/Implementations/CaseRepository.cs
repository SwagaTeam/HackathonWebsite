using HackathonWebsite.DTO.Case;
using HackathonWebsite.DTO.Hackaton;
using HackathonWebsite.Mapper;
using Microsoft.EntityFrameworkCore;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public class CaseRepository(AppDbContext dbContext) : ICaseRepository
    {
        public async Task<int> Create(CaseDto @case)
        {
            var existingHackaton = await dbContext.Hackathons.FirstOrDefaultAsync(x=>x.Id == @case.HackathonId);

            if (existingHackaton is null)
                throw new NullReferenceException($"Не существует хакатона с Id {@case.HackathonId}");

            await dbContext.Cases.AddAsync(CaseMapper.CaseDtoToCaseEntity(@case));

            await dbContext.SaveChangesAsync();

            return @case.Id;
        }

        public async Task<int> Delete(int id)
        {
            var existingCase = await dbContext.Cases.FindAsync(id);

            if(existingCase is null)
                throw new NullReferenceException($"Не существует кейса Id {id}");

            dbContext.Remove(existingCase);

            return id;
        }

        public async Task<CaseDto> GetById(int id)
        {
            var existingCase = await dbContext.Cases.FindAsync(id);

            if (existingCase is null)
                throw new NullReferenceException($"Не существует кейса Id {id}");

            return CaseMapper.CaseDtoToCaseEntity(existingCase);
        }

        public async Task<CaseDto> GetByName(string name)
        {
            var existingCase = await dbContext.Cases.FirstOrDefaultAsync(x => x.Title == name);

            if (existingCase is null)
                throw new NullReferenceException($"Не существует кейса с названием {name}");

            return CaseMapper.CaseDtoToCaseEntity(existingCase);
        }

        public async Task<int> Update(CaseDto @case)
        {
            var entity = await dbContext.Cases.FirstOrDefaultAsync(x => x.Title == @case.Title);

            if (entity is not null)
            {
                entity.Title = @case.Title;
                entity.Description = @case.Description;
                entity.Author = @case.Author;
            }
            else
                throw new NullReferenceException($"Не существует кейса с айди {@case.Id}");

            return @case.Id;
        }
    }
}
