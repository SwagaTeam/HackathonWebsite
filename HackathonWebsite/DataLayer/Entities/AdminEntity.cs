namespace HackathonWebsite.DataLayer.Entities
{
    public class AdminEntity
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
