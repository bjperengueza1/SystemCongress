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
    
    Task<CongressDto> ActiveAsync(int id);
    
    Task<CongressDto> GetActivesAsync();

    Task<bool> FileCertificateAttendance(int id, string fileName);
    
    Task<bool> FileCertificateExposure(int id, string fileName);
    
    Task<bool> FileCertificateConference(int id, string fileName);
    
    Task<bool> FileFlayer(int id, string fileName);
    
    Task<Stream> GetFlayerActiveCongressAsync(string directorio);

}
