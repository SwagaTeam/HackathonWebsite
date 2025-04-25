using System.ComponentModel;

namespace HackathonWebsite.DTO.Auth.UserAuth;

public class UserAuthDto : IUser
{
    public int? Id { get; set; }
    public string FullName { get; set; }
    public string University { get; set; }
    public string Email { get; set; }
    public string Telegram { get; set; }
    public string Password { get; set; }

    [DefaultValue(null)]
    public int? TeamId { get; set; }

    public string UniqueId => Id.ToString();
}