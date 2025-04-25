using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.DTO.Auth.UserAuth;
using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers.Auth;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService, ILogger<AuthController> logger) : ControllerBase
{
    [HttpGet("get-current-user")]
    public async Task<IActionResult> GetCurrentUser()
    {
        try
        {
            var userId = authService.GetCurrentUserId();
            var userRoles = authService.GetCurrentUserRoles();

            if (userId == -1) return Unauthorized("Not logged in");
            return Ok(new {id = userId, roles = userRoles });
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserAuthDto user)
    {
        try
        {
            var userId = await authService.Register(user);
            logger.LogInformation($"REGISTER: A new User with id \"{userId}\" has been created.");
            return Ok(userId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest("Registration failed " + ex.Message);
        }
    }

    [HttpPost("admin-register")]
    public async Task<IActionResult> AdminRegister([FromBody] AdminAuthDto user)
    {
        try
        {
            var userId = await authService.AdminRegister(user);
            logger.LogInformation($"REGISTER: A new User with id \"{userId}\" has been created.");
            return Ok(userId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest("Registration failed " + ex.Message);
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        try
        {
            if (authService.GetCurrentUserId() != -1)
                throw new UnauthorizedAccessException("You are already logged in");
            var user = await authService.Login(email, password, false);
            if (user is null) return Unauthorized("Invalid number/password");
            var token = authService.GenerateJwtToken(user);
            logger.LogInformation($"LOGGED IN: Token {token}");
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("admin-login")]
    public async Task<IActionResult> AdminLogin(string nickName, string password)
    {
        try
        {
            if (authService.GetCurrentUserId() != -1)
                throw new UnauthorizedAccessException("You are already logged in");
            var user = await authService.Login(nickName, password, true);
            if (user is null) return Unauthorized("Invalid number/password");
            var token = authService.GenerateJwtToken(user);
            logger.LogInformation($"LOGGED IN: Token {token}");
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        var token = authService.GetToken();
        if (string.IsNullOrEmpty(token)) return BadRequest("Invalid token");
        authService.Logout(token);
        return Ok("Logged out");
    }
}