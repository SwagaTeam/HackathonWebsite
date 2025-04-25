namespace HackathonWebsite.BusinessLayer.Services.AuthService.Abstractions;

public interface IEncrypt
{
    public string HashPassword(string password, string salt);
}