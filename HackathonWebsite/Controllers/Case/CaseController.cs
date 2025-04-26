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
        public async Task<IActionResult> Create([FromBody] CaseDto dto)
        {
            try
            {
                return Ok(await caseService.Create(dto));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{int id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await caseService.Delete(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("update")]
        public async Task<IActionResult> Update([FromBody] CaseDto dto)
        {
            try
            {
                return Ok(await caseService.Create(dto));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await caseService.Get());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await caseService.GetById(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                return Ok(await caseService.GetByName(name));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
