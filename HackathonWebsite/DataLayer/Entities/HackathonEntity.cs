namespace HackathonWebsite.DataLayer.Entities
{
    public class HackathonEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ParticipantsCount { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CaseEntity> Cases { get; set; }
    }
}
