namespace HackathonWebsite.DataLayer.Entities
{
    public class ApplyToHackEntity
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int CaseId { get; set; }
        public string Description { get; set; }
        public bool IsApplied { get; set; }
        public virtual TeamEntity Team { get; set; }
        public virtual CaseEntity Case { get; set; }
    }
}
