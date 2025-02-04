using Domain.Entities.Enums;

namespace Application.Exposures.DTOs;

public class ExposureRejectDto
{
    public StatusExposure StatusExposure { get; set; } = StatusExposure.Rejected;
    public string Observation { get; set; } = string.Empty;
}