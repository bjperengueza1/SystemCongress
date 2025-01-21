using Application.Common;
using Application.Exposures.DTOs;

namespace Application.Exposures.Interfaces;

public interface IExposureService : ICommonService<ExposureWitchAuthorsDto, ExposureInsertDto, ExposureUpdateDto>
{
    Task<ExposureWitchAuthorsDto> ChangeStatusAsync(int id, ExposureUpdateStatusDto exposureUpdateStatusDto);
    
    Task<ExposureWitchAuthorsDto> GetByGuidAsync(string guid);
}