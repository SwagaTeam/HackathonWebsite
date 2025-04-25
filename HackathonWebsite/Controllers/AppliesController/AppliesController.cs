using HackathonWebsite.BusinessLayer.Services.ApplyService;
using HackathonWebsite.BusinessLayer.Services.AuthService;
using HackathonWebsite.DTO;
using HackathonWebsite.DTO.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers.AppliesController
{
    [ApiController]
    [Route("applies")]
    public class AppliesController(IApplyService applyService, IAuthService authService) : ControllerBase
    {
        [HttpGet("hackaton-applies/get")]
        public async Task<IActionResult> GetHackApplies()
        {
            var role = authService.GetCurrentUserRoles();

            if (role == Roles.ADMIN)
            {
                var applies = applyService.GetApplyToHack();
                return Ok(applies);
            }

            return Unauthorized("Not enough permissions");
        }

        [HttpPost("create-hack-apply")]
        public async Task<IActionResult> CreateHackatonApply(ApplyToHackDto dto)
        {
            var currentId = authService.GetCurrentUserId();

            if(currentId != -1)
            {
                await applyService.CreateApplyHack(dto);
                return Ok();
            }

            return Unauthorized("Not authorized");
        }

        [HttpPost("approve-hack-apply/{id}")]
        public async Task<IActionResult> ApproveHackatonApply(int id)
        {
            var role = authService.GetCurrentUserRoles();

            if (role == Roles.ADMIN)
            {
                await applyService.ApproveApplyHack(id);
                return Ok();
            }

            return Unauthorized("Not enough permissions");
        }
    }
}
