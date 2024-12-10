using Domain.Entities.Enums;

namespace Web.Api.DTOs;

public class ExposureInsertFormDto
{
    public string NameExposure { get; set; }
    //StatusExposure by default is "Pending"
    private StatusExposure StatusExposure { get; set; }
    public ResearchLine ResearchLine { get; set; }
    public int CongressId { get; set; }
    public string Authors { get; set; }
    
    public ExposureInsertFormDto()
    {
        StatusExposure = StatusExposure.Pending;
    }
}