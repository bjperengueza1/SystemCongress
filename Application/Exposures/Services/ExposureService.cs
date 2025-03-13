using Application.Common;
using Application.Exposures.DTOs;
using Application.Exposures.Interfaces;
using Application.Files.Interfaces;
using AutoMapper;
using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Filter;
using Domain.Interfaces;

namespace Application.Exposures.Services;

public class ExposureService : IExposureService
{
    private readonly IExposureRepository _exposureRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IEmailService _emailService;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    
    public ExposureService(
        IExposureRepository exposureRepository,
        IAuthorRepository authorRepository,
        IFileService fileService,
        IEmailService emailService,
        IMapper mapper)
    {
        _exposureRepository = exposureRepository;
        _authorRepository = authorRepository;
        _fileService = fileService;
        _emailService = emailService;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ExposureWitchAuthorsDto>> GetAllAsync()
    {
        var exposures = await _exposureRepository.GetAllAsync();
        
        return exposures.Select(c => _mapper.Map<ExposureWitchAuthorsDto>(c));
    }

    public async Task<ExposureWitchAuthorsDto> GetByIdAsync(int id)
    {
        var exposure = await _exposureRepository.GetByIdAsync(id);
        
        if (exposure == null) return null;
        
        return _mapper.Map<ExposureWitchAuthorsDto>(exposure);
    }

    public async Task<ExposureWitchAuthorsDto> CreateAsync(ExposureInsertDto ti)
    {
        var exposure = _mapper.Map<Exposure>(ti);
        
        foreach (var t in ti.Authors)
        {
            var author = await _authorRepository.GetByIdNumberAsync(t.IDNumber);
            if ( author == null)
            {
                author = _mapper.Map<Author>(t);
                await _authorRepository.AddAsync(author);
            }

            exposure.ExposureAuthor.Add(
                new ExposureAuthor
                {
                    Author = author
                });
        }

        await _authorRepository.SaveAsync();
        
        await _exposureRepository.AddAsync(exposure);
        await _exposureRepository.SaveAsync();
        
        return _mapper.Map<ExposureWitchAuthorsDto>(exposure);
    }

    public async Task<ExposureWitchAuthorsDto> UpdateAsync(int id, ExposureUpdateDto tu)
    {
        var exposure = await _exposureRepository.GetByIdAsync(id);
        
        if (exposure == null) return null;
        
        exposure = _mapper.Map(tu, exposure);
        
        _exposureRepository.UpdateAsync(exposure);
        await _exposureRepository.SaveAsync();
        
        return _mapper.Map<ExposureWitchAuthorsDto>(exposure);
    }

    public async Task<PaginatedResult<ExposureWitchAuthorsDto>> GetPagedAsync(ExposureFilter tf)
    {
        var pagedData = await _exposureRepository.GetPagedAsync(tf);
        
        return pagedData.Map(p => _mapper.Map<ExposureWitchAuthorsDto>(p));
    }

    public async Task<ExposureWitchAuthorsDto> ChangeStatusAsync(int id, ExposureUpdateStatusDto exposureUpdateStatusDto)
    {
        //Traigo el objeto
        var exposure = await _exposureRepository.GetByIdAsync(id);
        
        if (exposure == null) return null;
        
        //Y lo que coincida lo actualizo
        exposure = _mapper.Map(exposureUpdateStatusDto, exposure);
        
        _exposureRepository.UpdateAsync(exposure);

        await _exposureRepository.SaveAsync();
        
        // Enviar correo al actualizar el estado
        var emailSent = await _emailService.SendStatusChangeNotificationAsync(exposure);
        
        return _mapper.Map<ExposureWitchAuthorsDto>(exposure);
    }

    public async Task<ExposureWitchAuthorsDto> GetByGuidAsync(string guid)
    {
        var exposure = await _exposureRepository.GetByGuidAsync(guid);
        
        return exposure == null ? null : _mapper.Map<ExposureWitchAuthorsDto>(exposure);
    }

    public async Task<ExposureWitchAuthorsDto> ApproveAsync(int id, ExposureApproveDto exposureApproveDto)
    {
        var exposure = await _exposureRepository.GetByIdAsync(id);
        
        if (exposure == null) return null;
        
        var firstAuthor = await _authorRepository.GetFirstAuthorByExposureAsync(exposure.ExposureId);
        
        exposure = _mapper.Map(exposureApproveDto, exposure);
        
        _exposureRepository.UpdateAsync(exposure);
        
        await _exposureRepository.SaveAsync();
        
        // Enviar correo al aprobar
        var email = firstAuthor.PersonalMail;
        var subject = "Aprobada";
        var nombreDestinatario = firstAuthor.Name.ToUpper();
        var tituloPonencia = exposure.Name.ToUpper();
        var nombreEvento = exposure.Congress.Name.ToUpper();
        var nombreOrganizacion = "Instituto Los Andes";
        var urlEvento = "https://www.tech2025.com";
        var correoContacto = "contacto@tech2025.com";
        var observacion = exposureApproveDto.Observation;

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
                         <h2>¡Felicidades! Tu ponencia ha sido aprobada</h2>
                         <p>Estimado/a {{nombreDestinatario}},</p>
                         <p>Nos complace informarte que tu ponencia titulada '<strong>{{tituloPonencia}}</strong>' ha sido aprobada para formar parte del programa de nuestro evento {{nombreEvento}}.</p>
                         <p>Te agradecemos por tu participación y por compartir tu conocimiento con la comunidad. Próximamente, te enviaremos más detalles sobre el evento, la fecha y el horario en el que presentarás tu ponencia.</p>
                         <p>Si tienes alguna duda o necesitas más información, no dudes en contactarnos.</p>
                         <p>¡Enhorabuena nuevamente y nos vemos pronto!</p>
                         <p><strong>Observación:</strong></p>
                         <p>{{observacion}}</p>
                         <p>Atentamente,</p>
                         <p><strong>
                         {{nombreOrganizacion}}</p>
                 
                         <div class='footer'>
                             <p>Si tienes alguna pregunta, puedes visitar nuestro sitio web <a href='{{urlEvento}}'>aquí</a> o escribirnos a {{correoContacto}}.</p>
                         </div>
                     </div>
                 </body>
                 </html>
                 """;

        var emailSent = await _emailService.SendEmailAsync(email, subject, bodyHTML);
        
        Console.WriteLine(emailSent);
        
        return _mapper.Map<ExposureWitchAuthorsDto>(exposure);
    }

    public async Task<ExposureWitchAuthorsDto> RejectAsync(int id, ExposureRejectDto exposureRejectDto)
    {
        var exposure = await _exposureRepository.GetByIdAsync(id);
        
        if (exposure == null) return null;
        
        var firstAuthor = await _authorRepository.GetFirstAuthorByExposureAsync(exposure.ExposureId);
        
        exposure = _mapper.Map(exposureRejectDto, exposure);
        
        _exposureRepository.UpdateAsync(exposure);
        
        await _exposureRepository.SaveAsync();
        
        // Enviar correo al rechazar
        var email = firstAuthor.PersonalMail;
        var subject = "Rechazada";
        var nombreDestinatario = firstAuthor.Name.ToUpper();
        var tituloPonencia = exposure.Name.ToUpper();
        var nombreEvento = exposure.Congress.Name.ToUpper();
        var nombreOrganizacion = "Instituto Los Andes";
        var urlEvento = "https://www.tech2025.com";
        var correoContacto = "contacto@tech2025.com";
        var observacion = exposureRejectDto.Observation;
        
        var bodyHTML = $$"""

                         <!DOCTYPE html>
                         <html lang='es'>
                         <head>
                             <meta charset='UTF-8'>
                             <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                             <title>Notificación de Rechazo de Ponencia</title>
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
                                     color: #e74c3c;
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
                                     color: #e74c3c;
                                     text-decoration: none;
                                 }
                             </style>
                         </head>
                         <body>
                             <div class='container'>
                                 <h2>Notificación de Rechazo de Ponencia</h2>
                                 <p>Estimado/a {{nombreDestinatario}},</p>
                                 <p>Lamentamos informarte que tu ponencia titulada '<strong>{{tituloPonencia}}</strong>' no ha sido seleccionada para formar parte del programa de {{nombreEvento}}.</p>
                                 <p><strong>Motivo del rechazo:</strong></p>
                                 <p>{{observacion}}</p>
                                 <p>Recibimos una gran cantidad de propuestas, y la selección fue muy competitiva. Aunque tu trabajo es valioso, no ha sido posible incluirlo en esta ocasión.</p>
                                 <p>Agradecemos sinceramente tu interés en participar y te animamos a seguir contribuyendo a la comunidad. Esperamos poder contar contigo en futuros eventos.</p>
                                 <p>Si tienes alguna pregunta o deseas recibir retroalimentación sobre tu propuesta, no dudes en ponerte en contacto con nosotros.</p>
                                 <p>Gracias por tu comprensión, y te deseamos mucho éxito en tus futuros proyectos.</p>
                                 <p>Atentamente,</p>
                                 <p><strong>
                                 {{nombreOrganizacion}}</p>
                         
                                 <div class='footer'>
                                     <p>Para más información sobre futuros eventos, visita nuestro sitio web <a href='{{urlEvento}}'>aquí</a> o escríbenos a {{correoContacto}}.</p>
                                 </div>
                             </div>
                         </body>
                         </html>
                         """;
        
        var emailSent = await _emailService.SendEmailAsync(email, subject, bodyHTML);
        
        return _mapper.Map<ExposureWitchAuthorsDto>(exposure);
    }
    
    public async Task<ExposureWitchAuthorsDto> ReviewAsync(int id)
    {
        var exposure = await _exposureRepository.GetByIdAsync(id);
        
        if (exposure == null) return null;

        exposure.StatusExposure = StatusExposure.InReview;
        
        _exposureRepository.UpdateAsync(exposure);
        
        await _exposureRepository.SaveAsync();
        
        return _mapper.Map<ExposureWitchAuthorsDto>(exposure);
    }

    public async Task<PaginatedResult<ExposureWitchAuthorsDto>> GetByCongressAsync(int id, int pageNumber, int pageSize )
    {
        var pagedData = await _exposureRepository.GetExposuresByCongressPagedAsync(id, pageNumber, pageSize);
        
        return pagedData.Map(p => _mapper.Map<ExposureWitchAuthorsDto>(p));
    }
    
    public async Task<PaginatedResult<ExposureWitchAuthorsDto>> GetExposuresApprovedByCongress(int id, int pageNumber, int pageSize )
    {
        var pagedData = await _exposureRepository.GetExposuresApprovedByCongressPagedAsync(id, pageNumber, pageSize);
        
        return pagedData.Map(p => _mapper.Map<ExposureWitchAuthorsDto>(p));
    }

    public async Task<bool> RegisterPreviousAsync(int id, string email)
    {
        var exposure = await _exposureRepository.GetByIdAsync(id);
        
        if (exposure == null) return false;
        
        // Enviar correo al registrar previo registro
        var subject = "Registro de Previa Participación";
        var nombreDestinatario = exposure.Name.ToUpper();
        var tituloPonencia = exposure.Name.ToUpper();
        var nombreEvento = exposure.Congress.Name.ToUpper();
        var nombreOrganizacion = "Instituto Los Andes";
        var urlEvento = "https://www.tech2025.com";
        var correoContacto = "contacto@tech2025.com";
        var observacion = exposure.Observation;
        
        
        var bodyHTML = $$"""
                         
                         <!DOCTYPE html>
                         <html lang='es'>
                         <head>
                             <meta charset='UTF-8'>
                             <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                             <title>Notificación de Registro de Previa Participación</title>
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
                                 <h2>¡Felicidades! </h2>
                                 <p>Nos complace informarte que tu preregistro a ponencia titulada <strong>{{tituloPonencia}}</strong> ha sido aprobada en nuestro evento {{nombreEvento}}.</p>
                                 <p>Te agradecemos por tu participación y por compartir tu conocimiento con la comunidad. Próximamente, te enviaremos más detalles sobre el evento, la fecha y el horario en el que presentarás tu ponencia.</p>
                                 <p>Si tienes alguna duda o necesitas más información, no dudes en contactarnos.</p>
                                 <p>¡Enhorabuena nuevamente y nos vemos pronto!</p>
                                 <p><strong>Observación:</strong></p>
                                 <p>{{observacion}}</p>
                                 <p>Atentamente,</p>
                                 <p><strong>
                                 {{nombreOrganizacion}}</p>
                         
                                 <div class='footer'>
                                     <p>Si tienes alguna pregunta, puedes visitar nuestro sitio web <a href='{{urlEvento}}'>aquí</a> o escribirnos a {{correoContacto}}.</p>
                                 </div>
                             </div>
                         </body>
                         </html>
                         
                         """;
        
        var emailSent = await _emailService.SendEmailAsync(email, subject, bodyHTML);
        
        return emailSent;

    }

    public async Task<Stream> GetReportExcelAsync(ExposureFilter filter)
    {
        var data = await _exposureRepository.GetAllEAsync(filter);
        
        // Transformar los datos para que los autores estén en columnas separadas
        var dataTransformed = data.Select(e => 
        {
            var authors = e.ExposureAuthor
                .OrderBy(ea => ea.Position) // Ordenamos los autores por "position"
                .Select(ea => ea.Author) // Obtenemos el nombre del autor
                .ToList();
            
            //return list strings
            return new List<string>
            {
                e.Name,
                e.StatusExposure.ToString(),
                e.ResearchLine.ToString(),
                e.Type.ToString(),
                e.DateStart.ToString(),
                e.DateEnd.ToString(),
                e.Observation,
                e.Congress.Name,
                authors.Count > 0 ? authors[0].Name : "",
                authors.Count > 1 ? authors[1].Name : "",
                authors.Count > 2 ? authors[2].Name : ""
            };
        }).ToList();
        
        var headers = new List<string>
        {
            "Nombre",
            "Estado",
            "Línea de Investigación",
            "Tipo",
            "Fecha Inicio",
            "Fecha Fin",
            "Observación",
            "Congreso",
            "Autor 1",
            "Autor 2",
            "Autor 3"
        };
        
        var excel = await _fileService.CreateExcelStream(headers, dataTransformed);

        return excel;
        
    }
}