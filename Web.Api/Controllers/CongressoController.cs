using Application.Congress.Commands;
using Application.Congress.DTOs;
using Application.Congress.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongressoController : ControllerBase
    {
        private readonly ICongressService _congressService;
        
        public CongressoController(ICongressService congressService)
        {
            _congressService = congressService;
        }
        
        [HttpPost]
        public async Task<ActionResult> AddCongresso([FromBody] CongressInsertDto insertDto)
        {
            var congressDto = await _congressService.CreateAsync(insertDto);
            
            return CreatedAtAction(nameof(AddCongresso), new { id = congressDto.CongressID}, congressDto);
        }
    }
}
