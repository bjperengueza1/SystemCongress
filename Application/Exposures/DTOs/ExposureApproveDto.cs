using Domain.Entities.Enums;

namespace Application.Exposures.DTOs;

public class ExposureApproveDto
{
    public int RoomId { get; set; }
    public StatusExposure StatusExposure { get; set; } = StatusExposure.Approved;
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string Observation { get; set; } = string.Empty;
    public string UrlAccess { get; set; } = string.Empty;
}