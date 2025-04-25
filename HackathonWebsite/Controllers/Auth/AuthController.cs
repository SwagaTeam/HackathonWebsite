using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers.Auth;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService, ILogger<AuthController> logger) : ControllerBase
{
    public async Task<IActionResult> GetCurrentUser()
    {
        try
        {
            var userId = authService.GetCurrentUserId();
            if (userId == -1) return Unauthorized("Not logged in");
            return Ok(userId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

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
            return BadRequest("Registration failed");
        }
    }

    public async Task<IActionResult> Login(string email, string password)
    {
        try
        {
            if (authService.GetCurrentUserId() != -1)
                throw new UnauthorizedAccessException("You are already logged in");
            var user = await authService.Login(email, password);
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

    public IActionResult Logout()
    {
        var token = authService.GetToken();
        if (string.IsNullOrEmpty(token)) return BadRequest("Invalid token");
        authService.Logout(token);
        return Ok("Logged out");
    }
}