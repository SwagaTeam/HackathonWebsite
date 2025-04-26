using HackathonWebsite.BusinessLayer.Services.MailService;
using HackathonWebsite.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public class ApplyTeamRepository(AppDbContext dbContext) : IApplyTeamRepository
    {
        public async Task<ApplyToTeamEntity> GetById(int applyToTeamId)
        {
            return await dbContext.ApplyToTeams.FirstOrDefaultAsync(x => x.Id == applyToTeamId);
        }
        public async Task<int> CreateApply(ApplyToTeamEntity entity)
        {
            await dbContext.ApplyToTeams.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<ICollection<ApplyToTeamEntity>> Get()
        {
            return await dbContext.ApplyToTeams.ToListAsync();
        }

        public async Task Apply(int applyId)
        {
            var apply = await dbContext.ApplyToTeams.FirstOrDefaultAsync(x => x.Id == applyId);
            apply.IsApplied = true;
            await dbContext.SaveChangesAsync();
        }
    }
}
