using HackathonWebsite.BusinessLayer.Services.HackathonService;
using HackathonWebsite.DTO.Auth;
using HackathonWebsite.DTO.Auth.UserAuth;
using HackathonWebsite.DTO.Hackaton;
using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers.Hackaton
{
    [ApiController]
    [Route("hackaton")]
    public class HackatonController(IHackathonService hackathonService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<int> Create([FromBody] HackatonDto dto)
        {
            return await hackathonService.Create(dto);
        }

        [HttpDelete("delete/{int id}")]
        public async Task<int> Delete(int id)
        {
            return await hackathonService.Delete(id);
        }

        [HttpPatch("update")]
        public async Task<int> Update([FromBody] HackatonDto dto)
        {
            return await hackathonService.Create(dto);
        }

        [HttpGet("get/{id}")]
        public async Task<HackatonDto> GetById(int id)
        {
            return await hackathonService.GetById(id);
        }

        [HttpGet("get/name/{name}")]
        public async Task<HackatonDto> GetByName(string name)
        {
            return await hackathonService.GetByName(name);
        }
    }
}
