using Application.Common;
using Application.Exposures.DTOs;

namespace Application.Exposures.Interfaces;

public interface IExposureService : ICommonService<ExposureDto, ExposureInsertDto, ExposureUpdateDto>
{
    
}