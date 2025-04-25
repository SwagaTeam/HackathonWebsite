namespace HackathonWebsite.DTO;

public class UserAuthDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string University { get; set; }
    public string Email { get; set; }
    public string Telegram { get; set; }
    public string Password { get; set; }
    public int? TeamId { get; set; } 
}