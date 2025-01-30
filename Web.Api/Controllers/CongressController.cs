using Application.Congresses.DTOs;
using Application.Congresses.Interfaces;
using Domain.Common.Pagination;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongressController : ControllerBase
    {
        private readonly IValidator<CongressInsertDto> _congressInsertValidator;
        private readonly IValidator<CongressUpdateDto> _congressUpdateValidator;
        private readonly FileStorageSettings _fileStorageSettings;
        private readonly ICongressService _congressService;
        
        public CongressController(IValidator<CongressInsertDto> congressInsertValidator,
            IValidator<CongressUpdateDto> congressUpdateValidator,
            IOptions<FileStorageSettings> options,
            ICongressService congressService)
        {
            _congressInsertValidator = congressInsertValidator;
            _congressUpdateValidator = congressUpdateValidator;
            _fileStorageSettings = options.Value;
            _congressService = congressService;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginatedResult<CongressDto>>> GetCongresses([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
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
        public async Task<IActionResult> AddCongress([FromBody] CongressInsertDto insertDto)
        {
            var validationResult = await _congressInsertValidator.ValidateAsync(insertDto);
            if (!validationResult.IsValid)
            {
                
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();
                
                return BadRequest(new { Errors = errors });

            }
            
            var congressDto = await _congressService.CreateAsync(insertDto);
            
            return CreatedAtAction(nameof(GetCongresses), new { id = congressDto.CongressId}, null);
        }
        
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateCongress(int id, [FromBody] CongressUpdateDto updateDto)
        {
            var validationResult = await _congressUpdateValidator.ValidateAsync(updateDto);
            
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();
                
                return BadRequest(new { Errors = errors });
            }
            
            var congressDto = await _congressService.UpdateAsync(id, updateDto);
            
            return congressDto == null ? NotFound() : Ok(congressDto);
        }
        
        //get by guid
        [HttpGet("guid/{guid}")]
        [Authorize]
        public async Task<ActionResult<CongressDto>> GetCongressByGuid(string guid)
        {
            var congressDto = await _congressService.GetByGuidAsync(guid);
            
            return congressDto == null ? NotFound() : Ok(congressDto);
        }
        
        //get list of certificates congress by dni
        [HttpGet("certificates/{dni}")]
        public async Task<ActionResult<IEnumerable<CongressCertificate>>> GetCertificatesByDni(string dni)
        {
            var congresses = await _congressService.GetCertificatesByDniAsync(dni);
            
            return Ok(congresses);
        }
        
        
        //download certificate congress
        [HttpGet("certificate/{id}/{dni}")]
        public async Task<IActionResult> DownloadCertificate(int id,string dni)
        {
            
            var file = await _congressService.DownloadCertificateAttendanceAsync(id, dni, _fileStorageSettings.TemplateCertificatesPath);
            
            if (file == null)
            {
                return NotFound();
            }
            
            return File(file, "application/pdf", "certificate.pdf");
        }
    }
}
