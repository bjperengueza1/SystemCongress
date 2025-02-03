using Application.Common;
using Application.Congresses.DTOs;
using Domain.Dtos;
using Domain.Entities;

namespace Application.Congresses.Interfaces;


public interface ICongressService : ICommonService<CongressDto, CongressInsertDto, CongressUpdateDto>
{
    Task<CongressDto> GetByGuidAsync(string guid);
    
    Task<IEnumerable<CongressCertificate>> GetCertificatesByDniAsync(string dni);
    
    Task<Stream> DownloadCertificateAttendanceAsync(int congressId, string dni, string directorio);
    Task<Stream> DownloadCertificateExposureAsync(int exposureId, string dni, string directorio);
    Task<Stream> DownloadCertificateConferenceAsync(int exposureId, string dni, string directorio);
    
}
