using Application.Common;
using Application.Exposures.DTOs;

namespace Application.Exposures.Interfaces;

public interface IExposureService : ICommonService<ExposureWitchAuthorsDto, ExposureInsertDto, ExposureUpdateDto>
{
    Task<ExposureWitchAuthorsDto> ChangeStatusAsync(int id, ExposureUpdateStatusDto exposureUpdateStatusDto);
    
    Task<ExposureWitchAuthorsDto> GetByGuidAsync(string guid);
    
    Task<ExposureWitchAuthorsDto> ApproveAsync(int id, ExposureApproveDto exposureApproveDto);
    
    Task<ExposureWitchAuthorsDto> RejectAsync(int id, ExposureRejectDto exposureRejectDto);
}