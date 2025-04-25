using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.DTO.Case;

namespace HackathonWebsite.Mapper
{
    public static class CaseMapper
    {
        public static CaseEntity CaseDtoToCaseEntity(CaseDto dto)
        {
            return new CaseEntity
            {
                Description = dto.Description,
                Author = dto.Author,
                Title = dto.Title,
                HackathonId = (int)dto.HackathonId
            };
        }

        public static CaseDto CaseDtoToCaseEntity(CaseEntity dto)
        {
            return new CaseDto
            {
                Id = dto.Id,
                Description = dto.Description,
                Author = dto.Author,
                Title = dto.Title,
                HackathonId = (int)dto.HackathonId
            };
        }
    }
}
