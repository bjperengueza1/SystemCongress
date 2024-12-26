using Application.Congresses.DTOs;
using Application.Congresses.Interfaces;
using Domain.Common.Pagination;
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
        public async Task<ActionResult<PagedResult<CongressDto>>> GetCongressos([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("El número de página y el tamaño deben ser mayores a 0.");
            }
            
            var congressos = await _congressService.GetPagedAsync(pageNumber, pageSize);
            
            return Ok(congressos);
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
            
            return CreatedAtAction(nameof(GetCongressos), new { id = congressDto.CongressId}, null);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCongresso(int id, [FromBody] CongressUpdateDto updateDto)
        {
            var congressDto = await _congressService.UpdateAsync(id, updateDto);
            
            return congressDto == null ? NotFound() : Ok(congressDto);
        }
    }
}
