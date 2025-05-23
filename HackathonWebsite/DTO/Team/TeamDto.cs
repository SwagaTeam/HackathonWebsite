using System.Text.Json.Serialization;
using HackathonWebsite.DataLayer.Entities;
using System.Text.Json.Serialization;

namespace HackathonWebsite.Dto.Team;

public class TeamDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
    public int? LeaderId { get; set; }
    public string GitHubLink { get; set; }
    public string GoogleDiskLink { get; set; }
    public int CaseId { get; set; }
    [JsonIgnore]
    public ICollection<UserEntity> Participants { get; set; } = new List<UserEntity>();
}