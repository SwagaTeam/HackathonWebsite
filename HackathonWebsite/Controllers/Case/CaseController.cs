using HackathonWebsite.BusinessLayer.Services.CaseService;
using HackathonWebsite.BusinessLayer.Services.HackathonService;
using HackathonWebsite.DTO.Auth;
using HackathonWebsite.DTO.Auth.UserAuth;
using HackathonWebsite.DTO.Case;
using HackathonWebsite.DTO.Hackaton;
using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers.Hackaton
{
    [ApiController]
    [Route("case")]
    public class CaseController(ICaseService caseService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<int> Create([FromBody] CaseDto dto)
        {
            return await caseService.Create(dto);
        }

        [HttpDelete("delete/{int id}")]
        public async Task<int> Delete(int id)
        {
            return await caseService.Delete(id);
        }

        [HttpPatch("update")]
        public async Task<int> Update([FromBody] CaseDto dto)
        {
            return await caseService.Create(dto);
        }

        [HttpGet("get/{id}")]
        public async Task<CaseDto> GetById(int id)
        {
            return await caseService.GetById(id);
        }

        [HttpGet("get/name/{name}")]
        public async Task<CaseDto> GetByName(string name)
        {
            return await caseService.GetByName(name);
        }
    }
}
