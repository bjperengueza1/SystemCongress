using Application.Congresses.DTOs;
using Application.Congresses.Interfaces;
using Domain.Common.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongressController : ControllerBase
    {
        private readonly ICongressService _congressService;
        
        public CongressController(ICongressService congressService)
        {
            _congressService = congressService;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginatedResult<CongressDto>>> GetCongressos([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("El número de página y el tamaño deben ser mayores a 0.");
            }
            
            var congressos = await _congressService.GetPagedAsync(pageNumber, pageSize, search);
            
            return Ok(congressos);
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CongressDto>> GetCongresso(int id)
        {
            var congressDto = await _congressService.GetByIdAsync(id);
            
            return congressDto == null ? NotFound() : Ok(congressDto);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCongresso([FromBody] CongressInsertDto insertDto)
        {
            var congressDto = await _congressService.CreateAsync(insertDto);
            
            return CreatedAtAction(nameof(GetCongressos), new { id = congressDto.CongressId}, null);
        }
        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCongresso(int id, [FromBody] CongressUpdateDto updateDto)
        {
            var congressDto = await _congressService.UpdateAsync(id, updateDto);
            
            return congressDto == null ? NotFound() : Ok(congressDto);
        }
    }
}
