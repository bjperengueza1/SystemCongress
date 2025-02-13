using Application.Congresses.DTOs;
using Application.Congresses.Interfaces;
using Application.Exposures.DTOs;
using Application.Exposures.Interfaces;
using Application.Files.Interfaces;
using Domain.Common.Pagination;
using Domain.Dtos;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongressController : ControllerBase
    {
        private readonly IValidator<CongressInsertDto> _congressInsertValidator;
        private readonly IValidator<CongressUpdateDto> _congressUpdateValidator;
        private readonly FileStorageSettings _fileStorageSettings;
        private readonly IFileService _fileService;
        private readonly ICongressService _congressService;
        private readonly IExposureService _exposureService;

        public CongressController(IValidator<CongressInsertDto> congressInsertValidator,
            IValidator<CongressUpdateDto> congressUpdateValidator,
            IOptions<FileStorageSettings> options,
            IFileService fileService,
            ICongressService congressService,
            IExposureService exposureService)
        {
            _congressInsertValidator = congressInsertValidator;
            _congressUpdateValidator = congressUpdateValidator;
            _fileStorageSettings = options.Value;
            _fileService = fileService;
            _congressService = congressService;
            _exposureService = exposureService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginatedResult<CongressDto>>> GetCongresses(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string search = "")
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

            return CreatedAtAction(nameof(GetCongresses), new { id = congressDto.CongressId }, null);
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
        [HttpGet("certificate-attendance/{id:int}/{dni}")]
        public async Task<IActionResult> DownloadCertificateAttendance(int id, string dni)
        {

            var file = await _congressService.DownloadCertificateAttendanceAsync(id, dni,
                _fileStorageSettings.TemplateCertificatesPath);

            if (file == null)
            {
                return NotFound();
            }

            return File(file, "application/pdf");
        }

        //download certificate congress
        [HttpGet("certificate-exposure/{id:int}/{dni}")]
        public async Task<IActionResult> DownloadCertificateExposure(int id, string dni)
        {

            var file = await _congressService.DownloadCertificateExposureAsync(id, dni,
                _fileStorageSettings.TemplateCertificatesPath);

            if (file == null)
            {
                return NotFound();
            }

            return File(file, "application/pdf");
        }

        //download certificate congress
        [HttpGet("certificate-conference/{id:int}/{dni}")]
        public async Task<IActionResult> DownloadCertificateConference(int id, string dni)
        {

            var file = await _congressService.DownloadCertificateConferenceAsync(id, dni,
                _fileStorageSettings.TemplateCertificatesPath);

            if (file == null)
            {
                return NotFound();
            }

            return File(file, "application/pdf");
        }

        //active congress
        [HttpPut("{id:int}/active")]
        [Authorize]
        public async Task<IActionResult> ActiveCongress(int id)
        {
            var congressDto = await _congressService.ActiveAsync(id);

            return congressDto == null ? NotFound() : Ok(congressDto);
        }

        //get congresses active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<CongressDto>>> GetActiveCongresses()
        {
            var congresses = await _congressService.GetActivesAsync();

            return Ok(congresses);
        }

        //get exposures by congress
        [HttpGet("{id:int}/exposures")]
        public async Task<ActionResult<IEnumerable<ExposureWitchAuthorsDto>>> GetExposuresByCongress(
            int id,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
            )
        {
            var exposures = await _exposureService.GetByCongressAsync(id, pageNumber, pageSize);
            
            return Ok(exposures);
        }
        
        //upload template certificate attendance
        [HttpPost("{id:int}/upload-template-certificate-attendance")]
        //[Authorize]
        public async Task<IActionResult> UploadTemplateCertificateAttendance(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No se ha enviado ningún archivo.");
            }
            
            var congress = await _congressService.GetByIdAsync(id);
            
            if (congress == null)
            {
                return BadRequest("El congreso no existe.");
            }
            
            var fileStream = new MemoryStream();
            
            await file.CopyToAsync(fileStream);
            
            FileUploaded fileUploaded;
            
            try
            {
                //mandar a borrar el archivo anterior
                fileUploaded = await _fileService.SaveFileAsync(file.FileName, fileStream,[".pdf"],_fileStorageSettings.TemplateCertificatesPath+"/"+congress.Guid );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("No se ha podido subir el archivo.");
            }
            

            //var result = await _congressService.UploadTemplateCertificateAttendanceAsync(id, file);

            return fileUploaded != null ? Ok() : BadRequest("No se ha podido subir el archivo.");
        }


    }
}
