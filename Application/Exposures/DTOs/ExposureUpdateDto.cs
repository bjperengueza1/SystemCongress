using Domain.Entities.Enums;

namespace Application.Exposures.DTOs;
public class ExposureUpdateDto
{
    public string Name { get; set; }
    //private StatusExposure Status { get; set; }
    public ResearchLine ResearchLine { get; set; }
    public string Observation { get; set; }
    public string UrlAccess { get; set; } = string.Empty;
    public int RoomId { get; set; }
}