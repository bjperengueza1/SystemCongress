using Application.Congresses.DTOs;
using Application.Congresses.Interfaces;
using Application.Files.Interfaces;
using AutoMapper;
using Domain.Common.Pagination;
using Domain.Dtos;
using Domain.Interfaces;
using Domain.Entities;
using Application.Common;
using Domain.Filter;

namespace Application.Congresses.Services;

public class CongressService : ICongressService
{
    
    private readonly ICongressRepository _congressRepository;
    private readonly IExposureRepository _exposureRepository;
    private readonly IAttendeeRepository _attendeeRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IFileService _fileService;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private ICongressService _congressServiceImplementation;

    public CongressService(
        ICongressRepository congressRepository,
        IExposureRepository exposureRepository,
        IAttendeeRepository attendeeRepository,
        IAuthorRepository authorRepository,
        IFileService fileService,
        IEmailService emailService,
        IMapper mapper)
    {
        _congressRepository = congressRepository;
        _exposureRepository = exposureRepository;
        _attendeeRepository = attendeeRepository;
        _authorRepository = authorRepository;
        _fileService = fileService;
        _emailService = emailService;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<CongressDto>> GetAllAsync()
    {
        var congresses = await _congressRepository.GetAllAsync();
        
        return congresses.Select(c => _mapper.Map<CongressDto>(c));
    }
    
    public async Task<CongressDto> GetByIdAsync(int id)
    {
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return null;
        
        return _mapper.Map<CongressDto>(congress);
    }

    public async Task<CongressDto> CreateAsync(CongressInsertDto insertDto)
    {
        var congress = _mapper.Map<Congress>(insertDto);
        
        await _congressRepository.AddAsync(congress);
        await _congressRepository.SaveAsync();
        
        return _mapper.Map<CongressDto>(congress);
    }
    
    public async Task<CongressDto> UpdateAsync(int id, CongressUpdateDto updateDto)
    {
        //Traigo el objeto
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return null;
        
        //Y lo que coincida lo actualizo
        congress = _mapper.Map(updateDto, congress);
        
        _congressRepository.UpdateAsync(congress);

        await _congressRepository.SaveAsync();
        
        return _mapper.Map<CongressDto>(congress);
    }

    public async Task<PaginatedResult<CongressDto>> GetPagedAsync(CongressFilter tf)
    {
        var pagedData = await _congressRepository.GetPagedAsync(tf);
        
        return pagedData.Map(p => _mapper.Map<CongressDto>(p));
    }

    public async Task<CongressDto> GetByGuidAsync(string guid)
    {
        var congress = await _congressRepository.GetByGuidAsync(guid);
        
        return congress == null ? null : _mapper.Map<CongressDto>(congress);
    }

    public async Task<IEnumerable<CongressCertificate>> GetCertificatesByDniAsync(string dni)
    {
        var congresses = await _congressRepository.GetCertificatesByDniAsync(dni);

        return congresses;
    }

    public async Task<Stream> DownloadCertificateAttendanceAsync(int congressId, string dni, string directorio)
    {
        var attendee = await _attendeeRepository.GetAttendeeByIdNumberAsync(dni);
        var congress = await _congressRepository.GetByIdAsync(congressId);
        
        if (congress == null || attendee == null) return null;
        
        var guid = Guid.NewGuid().ToString("N");
        var nameTemp = $"{dni}_{guid}.docx";

        string[] pathTemplate = [directorio,congress.Guid];

        await _fileService.CopyFileAsync(congress.fileCertificateAttendance, nameTemp, pathTemplate);
        
        var guidCertificate = await _attendeeRepository.GetGuidCertificateAttendanceAsync(congressId, attendee.AttendeeId);
        
        //lista de campos a reemplazar
        var fields = new Dictionary<string, string>
        {
            {"PERSONA", attendee.Name.ToUpper()},
            {"UUID", guidCertificate}
        };
        
        //Reemplaza el texto en el archivo
        _fileService.ReplaceTextInWord(nameTemp,pathTemplate,fields);
        
        var namePdf = _fileService.ConvertToPdf(nameTemp, pathTemplate);
        var certificado = await _fileService.GetFileAsync(namePdf, pathTemplate);

        //delete temp files
        await _fileService.DeleteFileAsync(nameTemp, pathTemplate);
        await _fileService.DeleteFileAsync(namePdf, pathTemplate);
        
        return certificado;
    }

    public async Task<Stream> DownloadCertificateExposureAsync(int exposureId, string dni, string directorio)
    {
        var author = await _authorRepository.GetByIdNumberAsync(dni);
        var exposure = await _exposureRepository.GetByIdAsync(exposureId);
        
        if (exposure == null || author == null) return null;
        
        var guid = Guid.NewGuid().ToString("N");
        var nameTemp = $"{dni}_{guid}.docx";
        
        string[] pathTemplate = [directorio,exposure.Congress.Guid];
        
        await _fileService.CopyFileAsync(exposure.Congress.fileCertificateExposure, nameTemp, pathTemplate);
        
        //lista de campos a reemplazar
        var fields = new Dictionary<string, string>
        {
            {"PERSONA", author.Name.ToUpper()},
            {"TEMA", exposure.Name.ToUpper()},
            {"UUID", exposure.GuidCert}
        };
        
        //Reemplaza el texto en el archivo
        _fileService.ReplaceTextInWord(nameTemp,pathTemplate,fields);
        
        var namePdf = _fileService.ConvertToPdf(nameTemp, pathTemplate);
        var certificado = await _fileService.GetFileAsync(namePdf, pathTemplate);

        //delete temp files
        await _fileService.DeleteFileAsync(nameTemp, pathTemplate);
        await _fileService.DeleteFileAsync(namePdf, pathTemplate);
        
        return certificado;
    }

    public async Task<Stream> DownloadCertificateConferenceAsync(int exposureId, string dni, string directorio)
    {
        var author = await _authorRepository.GetByIdNumberAsync(dni);
        var exposure = await _exposureRepository.GetByIdAsync(exposureId);
        
        if (exposure == null || author == null) return null;
        
        var guid = Guid.NewGuid().ToString("N");
        var nameTemp = $"{dni}_{guid}.docx";
        
        string[] pathTemplate = [directorio,exposure.Congress.Guid];
        
        await _fileService.CopyFileAsync(exposure.Congress.fileCertificateConference, nameTemp, pathTemplate);
        
        //lista de campos a reemplazar
        var fields = new Dictionary<string, string>
        {
            {"PERSONA", author.Name.ToUpper()},
            {"TEMA", exposure.Name.ToUpper()},
            {"UUID", exposure.GuidCert}
        };
        
        //Reemplaza el texto en el archivo
        _fileService.ReplaceTextInWord(nameTemp,pathTemplate,fields);
        
        var namePdf = _fileService.ConvertToPdf(nameTemp, pathTemplate);
        var certificado = await _fileService.GetFileAsync(namePdf, pathTemplate);

        //delete temp files
        await _fileService.DeleteFileAsync(nameTemp, pathTemplate);
        await _fileService.DeleteFileAsync(namePdf, pathTemplate);
        
        return certificado;
    }

    public async Task<CongressDto> ActiveAsync(int id)
    {
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return null;
        
        //get congresses active
        var congressActive = await _congressRepository.GetActiveAsync();

        if (congressActive != null)
        {
            //set inactive all congresses
            congressActive.Status = false; 
            _congressRepository.UpdateAsync(congressActive);
        }
        
        //active congress
        congress.Status = true;
        
        _congressRepository.UpdateAsync(congress);
        await _congressRepository.SaveAsync();
        
        return _mapper.Map<CongressDto>(congress);
    }

    public async Task<CongressDto> GetActivesAsync()
    {
        var congress = await _congressRepository.GetActiveAsync();
        
        return congress == null ? null : _mapper.Map<CongressDto>(congress);
    }

    public async Task<bool> FileCertificateAttendance(int id, string fileName)
    {
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return false;
        
        congress.fileCertificateAttendance = fileName;
        
        _congressRepository.UpdateAsync(congress);
        
        await _congressRepository.SaveAsync();
        
        return true;
    }

    public async Task<bool> FileCertificateExposure(int id, string fileName)
    {
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return false;
        
        congress.fileCertificateExposure = fileName;
        
        _congressRepository.UpdateAsync(congress);
        
        await _congressRepository.SaveAsync();
        
        return true;
    }
    
    public async Task<bool> FileCertificateConference(int id, string fileName)
    {
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return false;
        
        congress.fileCertificateConference = fileName;
        
        _congressRepository.UpdateAsync(congress);
        
        await _congressRepository.SaveAsync();
        
        return true;
    }

    public async Task<bool> FileFlayer(int id, string fileName)
    {
        var congress = await _congressRepository.GetByIdAsync(id);
        
        if (congress == null) return false;
        
        congress.fileFlayer = fileName;
        
        _congressRepository.UpdateAsync(congress);
        
        await _congressRepository.SaveAsync();
        
        return true;
    }

    public async Task<Stream> GetFlayerActiveCongressAsync(string directorio)
    {
        var congress = await _congressRepository.GetActiveAsync();
        
        if (congress == null) return null;
        
        string[] pathTemplate = [directorio,congress.Guid];
        
        return await _fileService.GetFileAsync(congress.fileFlayer, pathTemplate );
    }

    public async Task<bool> SendInvitationConferenceAsync(CongressDto congress, string[] emails)
    {
        var urlEvento = "https://cilai.istla-sigala.edu.ec";
        var correoContacto = "secretaria@istla.edu.ec";

        var bodyHTML = $$"""

                 <!DOCTYPE html>
                 <html lang='es'>
                 <head>
                     <meta charset='UTF-8'>
                     <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                     <title>Notificación de Aprobación de Ponencia</title>
                     <style>
                         body {
                             font-family: Arial, sans-serif;
                             background-color: #f4f4f9;
                             color: #333;
                             padding: 20px;
                         }
                         .container {
                             background-color: #ffffff;
                             border-radius: 8px;
                             padding: 20px;
                             box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
                         }
                         h2 {
                             color: #2e8b57;
                         }
                         p {
                             font-size: 16px;
                             line-height: 1.5;
                         }
                         .footer {
                             margin-top: 20px;
                             font-size: 14px;
                             color: #777;
                         }
                         .footer a {
                             color: #2e8b57;
                             text-decoration: none;
                         }
                     </style>
                 </head>
                 <body>
                     <div class='container'>
                         <h2>¡Te invitamos al Congreso!</h2>
                         <p>Es un honor para nosotros invitarte a participar en nuestro próximo congreso, un evento diseñado para reunir a profesionales, expertos y entusiastas de la industria.</p>
                         <p><strong>{{congress.Name}}</strong>.</p>
                         <p><strong>Ubicación:</strong> {{congress.Location}}</p>
                         <p><strong>Fecha:</strong> Del {{congress.StartDate:yyyy-MM-dd}} al {{congress.EndDate:yyyy-MM-dd}}</p>
                         <p>En este congreso, tendrás la oportunidad de asistir a conferencias magistrales impartidas por líderes en la materia. También podrás conectar con colegas y expandir tu red de contactos.</p>
                         <p>No pierdas esta oportunidad de actualizarte y ser parte de una comunidad vibrante e innovadora. ¡Tu presencia será fundamental para el éxito del evento!</p>
                         <a href="https://cilai.istla-sigala.edu.ec/registro-conferencia/{{congress.Guid}}" class="button">Regístrate aquí</a>
                 
                         <div class='footer'>
                             <p>Si tienes alguna pregunta, puedes visitar nuestro sitio web <a href='{{urlEvento}}'>aquí</a> o escribirnos a {{correoContacto}}.</p>
                         </div>
                     </div>
                 </body>
                 </html>
                 """;

        return await _emailService.SendEmailAsync(emails, "Invitación a Congreso", bodyHTML);
    }
}