using Application.Author.DTOs;
using Domain.Entities.Enums;

namespace Application.Exposures.DTOs;

public class ExposureInsertFormDto
{
    public string NameExposure { get; set; }
    //StatusExposure by default is "Pending"
    private StatusExposure StatusExposure = StatusExposure.Pending;
    public ResearchLine ResearchLine { get; set; }
    public int CongressId { get; set; }
    
    public string Authors { get; set; }
    
    
}