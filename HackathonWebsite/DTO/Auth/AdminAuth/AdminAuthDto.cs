namespace HackathonWebsite.DTO.Auth.AdminAuth
{
    public class AdminAuthDto : IUser
    {
        public int? Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string UniqueId => Id.ToString();
    }
}
