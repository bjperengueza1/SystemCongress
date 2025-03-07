using Application.Common;
using Application.Exposures.DTOs;
using Domain.Common.Pagination;
using Domain.Filter;

namespace Application.Exposures.Interfaces;

public interface IExposureService : ICommonService<ExposureWitchAuthorsDto, ExposureInsertDto, ExposureUpdateDto, ExposureFilter>
{
    Task<ExposureWitchAuthorsDto> ChangeStatusAsync(int id, ExposureUpdateStatusDto exposureUpdateStatusDto);
    
    Task<ExposureWitchAuthorsDto> GetByGuidAsync(string guid);
    
    Task<ExposureWitchAuthorsDto> ApproveAsync(int id, ExposureApproveDto exposureApproveDto);
    
    Task<ExposureWitchAuthorsDto> RejectAsync(int id, ExposureRejectDto exposureRejectDto);
    
    Task<ExposureWitchAuthorsDto> ReviewAsync(int id);
    
    Task<PaginatedResult<ExposureWitchAuthorsDto>> GetByCongressAsync(int id, int page, int size);
    
    Task<bool> RegisterPreviousAsync(int id, string email);
    
    Task<Stream> GetReportExcelAsync(ExposureFilter filter);
}