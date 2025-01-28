using System.Text.Json;
using Application.Authors.DTOs;
using Application.Congresses.Interfaces;
using Application.Exposures.DTOs;
using Application.Exposures.Interfaces;
using Application.Files.Interfaces;
using AutoMapper;
using Domain.Common.Pagination;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExposuresController : ControllerBase
    {
        private readonly IValidator<ExposureInsertDto> _exposureInsertValidator;
        private readonly IExposureService _exposureService;
        private readonly ICongressService _congressService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        
        
        public ExposuresController(
            IValidator<ExposureInsertDto> exposureInsertValidator,
            IExposureService exposureService,
            ICongressService congressService,
            IFileService fileService,
            IMapper mapper
            )
        {
            _exposureInsertValidator = exposureInsertValidator;
            _exposureService = exposureService;
            _congressService = congressService;
            _fileService = fileService;
            _mapper = mapper;
        }
        
        //get all
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ExposureWitchAuthorsDto>>> GetExposures(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string search = "")
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("El número de página y el tamaño deben ser mayores a 0.");
            }
            
            var exposures = await _exposureService.GetPagedAsync(pageNumber, pageSize, search);
            
            return exposures;
        }
        
        //get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ExposureWitchAuthorsDto>> GetExposure(int id)
        {
            var exposureDto = await _exposureService.GetByIdAsync(id);
            
            return exposureDto == null ? NotFound() : Ok(exposureDto);
        }
        
        
        //create exposure
        [HttpPost]
        public async Task<IActionResult> AddExposure(IFormFile pdfFile, [FromForm] ExposureInsertFormDto insertFormDto )
        {
            if (pdfFile == null || pdfFile.Length == 0)
            {
                return BadRequest("El archivo no es válido.");
            }
            // Convertir IFormFile a byte[]
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await pdfFile.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }
            
            FileUploaded fileUploaded;

            try
            {
                fileUploaded = await _fileService.UploadFileAsync(pdfFile.FileName, fileBytes);
            } catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            
            var congress = await _congressService.GetByGuidAsync(insertFormDto.CongressGuid);
            
            if (congress == null)
            {
                return BadRequest("El congreso no existe.");
            }
            
            var insertDto = _mapper.Map<ExposureInsertDto>(insertFormDto);
            
            insertDto.SummaryFilePath = fileUploaded.FileName;
            
            insertDto.CongressId = congress.CongressId;
            
            insertDto.Authors = JsonSerializer.Deserialize<List<AuthorInsertDto>>(insertFormDto.Authors);
            
            if(insertDto.Authors.Count == 0)
            {
                return BadRequest("Debe haber al menos un autor.");
            }
            
            var validationResult = await _exposureInsertValidator.ValidateAsync(insertDto);
            
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();
                
                return BadRequest(new { Errors = errors });
            }
            
            var exposureDto = await _exposureService.CreateAsync(insertDto);

            if (exposureDto == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(GetExposure), new { id = exposureDto.ExposureId}, null);
        }
        
        //update exposure
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateExposure(int id, [FromBody] ExposureUpdateDto updateDto)
        {
            var exposureDto = await _exposureService.UpdateAsync(id, updateDto);

            if (exposureDto == null)
            {
                return NotFound();
            }

            return Ok(exposureDto);
        }
        
        //aprobe or reject exposure
        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateStatusExposure(int id, [FromBody] ExposureUpdateStatusDto updateStatusDto)
        {
            var exposureDto = await _exposureService.ChangeStatusAsync(id, updateStatusDto);

            if (exposureDto == null)
            {
                return NotFound();
            }

            return Ok(exposureDto);
        }
        
        //dowload file summary
        [HttpGet("{id}/summary")]
        public async Task<IActionResult> DownloadSummary(int id)
        {
            var exposure = await _exposureService.GetByIdAsync(id);
            
            if (exposure == null)
            {
                return NotFound();
            }
            
            var file = await _fileService.GetFileAsync(exposure.SummaryFilePath);
            
            if (file == null)
            {
                return NotFound();
            }
            
            return File(file, "application/pdf", exposure.SummaryFilePath);
        }
        
        //get by guid
        [HttpGet("guid/{guid}")]
        public async Task<ActionResult<ExposureWitchAuthorsDto>> GetExposureByGuid(string guid)
        {
            var exposureDto = await _exposureService.GetByGuidAsync(guid);
            
            return exposureDto == null ? NotFound() : Ok(exposureDto);
        }
    }
}
