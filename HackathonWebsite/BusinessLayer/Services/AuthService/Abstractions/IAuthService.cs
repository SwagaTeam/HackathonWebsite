using HackathonWebsite.DTO.Auth.UserAuth;

namespace HackathonWebsite.BusinessLayer.Services.AuthService
{
    public interface IAuthService
    {
        public Task<int> Register(UserAuthDto user);
        public Task<int> AdminRegister(AdminAuthDto admin);
        public Task<IUser> Login(string email, string password, bool isAdmin);
        public int? GetCurrentUserId();
        public string GetCurrentUserRoles();
        public string GenerateJwtToken<T>(T user);
        public string GetToken();
        public void Logout(string token);
    }
}
