namespace HackathonWebsite.BusinessLayer.Services.AuthService.Abstractions;

public interface IBlackListService
{
    bool IsTokenBlacklisted(string token);
    void AddTokenToBlacklist(string token);
}
