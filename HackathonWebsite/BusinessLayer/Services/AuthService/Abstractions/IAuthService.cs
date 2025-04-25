using HackathonWebsite.DTO;

namespace HackathonWebsite.BusinessLayer.Services.AuthService
{
    public interface IAuthService
    {
        public Task<int> Register(UserAuthDto user);
        public Task<UserAuthDto> Login(string email, string password);
        public int? GetCurrentUserId();
        public string GenerateJwtToken<T>(T user);
        public string GetToken();
        public void Logout(string token);
    }
}
