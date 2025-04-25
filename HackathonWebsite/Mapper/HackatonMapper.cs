using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO.Hackaton;

namespace HackathonWebsite.Mapper
{
    public static class HackatonMapper
    {
        public static HackathonEntity HackathonDtoToEntity(HackatonDto dto)
        {
            return new HackathonEntity() 
            {
                Description = dto.Description,
                Title = dto.Title
            };
        }

        public static HackatonDto HackathonEntityToDto(HackathonEntity entity)
        {
            return new HackatonDto()
            {
                Id = entity.Id,
                Description = entity.Description,
                Title = entity.Title
            };
        }
    }
}
