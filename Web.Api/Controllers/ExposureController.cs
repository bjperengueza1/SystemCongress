using System.Text.Json;
using Application.Author.DTOs;
using Application.Exposures.DTOs;
using Application.Exposures.Interfaces;
using Application.Files.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Api.DTOs;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExposureController : ControllerBase
    {
        public readonly IExposureService _exposureService;
        public readonly IFileService _fileService;
        
        
        public ExposureController(
            IExposureService exposureService,
            IFileService fileService
            )
        {
            _exposureService = exposureService;
            _fileService = fileService;
        }
        
        [HttpPost]
        public async Task<ActionResult> AddExposure(
            IFormFile pdfFile,
            [FromForm] ExposureInsertFormDto insertFormDto
            )
        {

            var insertDto = new ExposureInsertDto()
            {
                NameExposure = insertFormDto.NameExposure,
                ResearchLine = insertFormDto.ResearchLine,
                CongressId = insertFormDto.CongressId,
                Authors = JsonSerializer.Deserialize<List<AuthorInsertDto>>(insertFormDto.Authors),
                SummaryFilePath = ""
            };
            
            
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
            
            var authors = insertDto.Authors;
            Console.WriteLine(authors.Count);
            foreach (var author in authors)
            {
                //Console.WriteLine(author.Name);
            }
            
            insertDto.SummaryFilePath = fileUploaded.FileName;
            var exposureDto = await _exposureService.CreateAsync(insertDto);

            if (exposureDto == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(AddExposure), new { id = exposureDto.ExposureId}, exposureDto);
        }
        
    }
}
