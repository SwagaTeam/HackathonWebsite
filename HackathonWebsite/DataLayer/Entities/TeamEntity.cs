namespace HackathonWebsite.DataLayer.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int? LeaderId { get; set; }
        public int CaseId { get; set; }
        
        public string GitHubLink { get; set; }
        public string GoogleDiskLink { get; set; }

        public virtual UserEntity Leader { get; set; }
        public virtual CaseEntity Case { get; set; }
        public virtual ICollection<ApplyToTeamEntity> AppliesToTeam { get; set; }
        public virtual ICollection<ApplyToHackEntity> AppliesToHack { get; set; }
        public virtual ICollection<UserEntity> Participants { get; set; } = new List<UserEntity>();
    }
}
