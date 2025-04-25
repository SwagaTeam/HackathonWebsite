namespace HackathonWebsite.DataLayer.Entities
{
    public class ApplyToTeamEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int TeamId { get; set; }
        public string Description { get; set; }
        public bool IsApplied { get; set; }
        public virtual UserEntity User {  get; set; }
        public virtual TeamEntity Team { get; set; }
    }
}
