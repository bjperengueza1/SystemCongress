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
        
        [HttpGet]
        public async Task<IEnumerable<CongressDto>> GetCongressos()
        {
            return await _congressService.GetAllAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CongressDto>> GetCongresso(int id)
        {
            var congressDto = await _congressService.GetByIdAsync(id);
            
            return congressDto == null ? NotFound() : Ok(congressDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddCongresso([FromBody] CongressInsertDto insertDto)
        {
            var congressDto = await _congressService.CreateAsync(insertDto);
            
            return CreatedAtAction(nameof(AddCongresso), new { id = congressDto.CongressId});
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCongresso(int id, [FromBody] CongressUpdateDto updateDto)
        {
            var congressDto = await _congressService.UpdateAsync(id, updateDto);
            
            return congressDto == null ? NotFound() : Ok(congressDto);
        }
    }
}
