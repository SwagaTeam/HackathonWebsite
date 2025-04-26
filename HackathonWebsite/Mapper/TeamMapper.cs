using HackathonWebsite.DataLayer.Entities;
using HackathonWebsite.Dto.Team;

namespace HackathonWebsite.Mapper;

public class TeamMapper
{
    public static TeamDto TeamToDto(TeamEntity team)
    {
        return new TeamDto
        {
            Id = team.Id,
            Link = team.Link,
            LeaderId = team.LeaderId,
            CaseId = team.CaseId,
            GitHubLink = team.GitHubLink,
            GoogleDiskLink = team.GoogleDiskLink,
            Title = team.Title,
            Description = team.Description,
        };
    }

    public static TeamEntity TeamToEntity(TeamDto teamDto)
    {
        return new TeamEntity
        {
            Id = teamDto.Id,
            Link = teamDto.Link,
            LeaderId = teamDto.LeaderId,
            CaseId = teamDto.CaseId,
            GitHubLink = teamDto.GitHubLink,
            GoogleDiskLink = teamDto.GoogleDiskLink,
            Title = teamDto.Title,
            Description = teamDto.Description,
        };
    }
}