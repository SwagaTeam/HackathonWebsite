namespace HackathonWebsite.DataLayer.Entities
{
    public class CaseEntity
    {
        public int Id { get; set; }
        public int HackathonId { get; set; }
        public string Title { get; set; } = "Default Title";
        public string Description { get; set; } = "Default Description";
        public string Author { get; set; } = "Default Author";

        public virtual ICollection<TeamEntity> Teams { get; set; }
        public virtual ICollection<ApplyToHackEntity> AppliesToHack { get; set; }

        public virtual HackathonEntity Hackathon { get; set; } = null!;
    }
}
