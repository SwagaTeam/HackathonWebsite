using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO;
using Microsoft.EntityFrameworkCore;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public class ApplyHackRepository(AppDbContext dbContext) : IApplyHackRepository
    {
        public async Task ApproveApply(int applyId)
        {
            var apply = await dbContext.ApplyToHacks
                .Include(x=>x.Team.Participants)
                .Include(x=>x.Case)
                .FirstOrDefaultAsync(x=>x.Id == applyId);

            if (apply is not null)
            {
                apply.IsApplied = true;

                var hackaton = await dbContext.Hackathons.FirstOrDefaultAsync(x => x.Id == apply.Case.HackathonId);
                hackaton!.ParticipantsCount += apply.Team.Participants.Count;

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> CreateApply(ApplyToHackEntity dto)
        {
            await dbContext.ApplyToHacks.AddAsync(dto);
            await dbContext.SaveChangesAsync();

            return dto.Id;

        }

        public async Task<ICollection<ApplyToHackEntity>> Get()
        {
            return await dbContext.ApplyToHacks.ToListAsync();
        }
    }
}
