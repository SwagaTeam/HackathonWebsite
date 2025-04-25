using HackathonWebsite.DTO.Hackaton;

namespace HackathonWebsite.DataLayer.Repositories.Implementations
{
    public class HackathonRepository(AppDbContext dbContext) : IHackathonRepository
    {
        public Task<int> Create(HackatonDto hackaton)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(HackatonDto hackaton)
        {
            throw new NotImplementedException();
        }

        public Task<HackatonDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HackatonDto> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(HackatonDto hackaton)
        {
            throw new NotImplementedException();
        }
    }
}
