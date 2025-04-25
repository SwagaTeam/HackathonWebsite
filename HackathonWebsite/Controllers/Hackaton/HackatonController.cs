using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.BusinessLayer.Services.HackathonService;
using HackathonWebsite.DTO.Auth;
using HackathonWebsite.DTO.Auth.UserAuth;
using HackathonWebsite.DTO.Hackaton;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HackathonWebsite.Controllers.Hackaton
{
    [ApiController]
    [Route("hackaton")]
    public class HackatonController(IHackathonService hackathonService, IAuthService authService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] HackatonDto dto)
        {
            var role = authService.GetCurrentUserRoles();

            if (role != Roles.ADMIN)
                return Unauthorized();
            return Ok(await hackathonService.Create(dto));
        }

        [HttpDelete("delete/{int id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = authService.GetCurrentUserRoles();

            if (role != Roles.ADMIN)
                return Unauthorized();

            return Ok(await hackathonService.Delete(id));
        }

        [HttpPatch("update")]
        public async Task<IActionResult> Update([FromBody] HackatonDto dto)
        {
            var role = authService.GetCurrentUserRoles();

            if (role != Roles.ADMIN)
                return Unauthorized();
            return Ok(await hackathonService.Update(dto));
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = authService.GetCurrentUserRoles();

            if (role != Roles.ADMIN)
                return Unauthorized();
            return Ok(await hackathonService.GetById(id));
        }

        [HttpGet("get/name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var role = authService.GetCurrentUserRoles();

            if (role != Roles.ADMIN)
                return Unauthorized();
            return Ok(await hackathonService.GetByName(name));
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            return Ok(await hackathonService.Get());
        }

        [HttpGet("get/active")]
        public async Task<IActionResult> GetActiveHackaton()
        {
            return Ok(await hackathonService.GetActiveHackaton());
        }

        [HttpPost("set/active/{id}")]
        public async Task<IActionResult> SetActiveHackaton(int id)
        {
            return Ok(await hackathonService.SetActiveHackaton(id));
        }
    }
}
