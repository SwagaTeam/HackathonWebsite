using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HackathonWebsite.BusinessLayer.Services.AuthService.Abstractions;
using HackathonWebsite.DataLayer.Repositories.Implementations;
using HackathonWebsite.DTO;
using HackathonWebsite.DTO.Auth;
using HackathonWebsite.DTO.Auth.AdminAuth;
using HackathonWebsite.DTO.Auth.UserAuth;
using HackathonWebsite.Mapper;
using Microsoft.IdentityModel.Tokens;

namespace HackathonWebsite.BusinessLayer.Services.AuthService.Implementations;

public class AuthService(
    IUserRepository repository,
    IEncrypt encrypt,
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration,
    IBlackListService blacklistService) : IAuthService
{
    public async Task<int> Register(UserAuthDto user)
    {
        var validator = new UserRegisterValidator();
        var result = await validator.ValidateAsync(user);
        if (!result.IsValid) 
            throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
        await repository.Create(UserMapper.UserAuthDtoToUserEntity(user));
        return (int)user.Id!;
    }

    public async Task<UserAuthDto> Login(string email, string password)
    {
        var user = await repository.GetByEmail(email);
        return user is not null && user.Password == encrypt.HashPassword(password, user.Salt)
            ? UserMapper.UserToUserAuthDto(user)
            : null!;
    }

    public List<string> GetCurrentUserRoles()
    {
        var claimsIdentity = httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
        return claimsIdentity?.FindAll(ClaimTypes.Role).Select(claim => claim.Value).ToList() ?? new List<string>();
    }

    public int? GetCurrentUserId()
    {
        var claimsIdentity = httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
        var id = int.Parse(claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "-1");
        return id;
    }

    public string GenerateJwtToken<T>(T user)
    {
        var userRoles = new List<string>();

        if (user is UserAuthDto userAuth)
            userRoles.Add(Roles.USER);
        else if (user is AdminAuthDto admin)
        {
            userRoles.Add(Roles.ADMIN);
        }
        else
            throw new ArgumentException("Неподдерживаемый тип пользователя");

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, (user as IUser)?.UniqueId ?? string.Empty),
            new Claim(ClaimTypes.NameIdentifier, (user as IUser)?.UniqueId.ToString() ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };


        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:ExpiresInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetToken()
    {
        return httpContextAccessor.HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Split().Last();
    }

    public void Logout(string token)
    {
        blacklistService.AddTokenToBlacklist(token);
    }
}