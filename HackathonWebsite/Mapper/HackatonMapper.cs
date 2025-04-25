using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO.Hackaton;

namespace HackathonWebsite.Mapper
{
    public static class HackatonMapper
    {
        public static HackathonEntity HackathonToEntity(HackatonDto dto)
        {
            return new HackathonEntity 
            {
                Description = dto.Description,
                Title = dto.Title, 
                IsActive = dto.IsActive
            };
        }

        public static HackatonDto HackathonToDto(HackathonEntity entity)
        {
            return new HackatonDto
            {
                Id = entity.Id,
                Description = entity.Description,
                Title = entity.Title,
                IsActive = entity.IsActive
            };
        }
    }
}
