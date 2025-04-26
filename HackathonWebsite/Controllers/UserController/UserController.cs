using HackathonWebsite.BusinessLayer.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers.UserController
{
    [ApiController]
    [Route("users")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id) 
        {
            try
            {
                return Ok(await userService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/set-role")]
        public async Task<IActionResult> GetUser(int id, [FromBody]string role)
        {
            try
            {
                return Ok(await userService.SetRole(id, role));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
