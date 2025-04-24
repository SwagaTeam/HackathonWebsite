namespace HackathonWebsite.DataLayer.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string University { get; set; }
        public string Email { get; set; }
        public string Telegram { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public int? TeamId { get; set; }
        public virtual TeamEntity? Team { get; set; }
        public virtual ICollection<ApplyToTeamEntity> AppliesToTeam { get; set; }
        public virtual ICollection<ApplyToHackEntity> AppliesToHack { get; set; }
    }
}
