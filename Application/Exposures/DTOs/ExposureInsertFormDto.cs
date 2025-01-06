using Domain.Entities.Enums;

namespace Application.Exposures.DTOs;

public class ExposureInsertFormDto
{
    public string Name { get; set; }
    private StatusExposure Status = StatusExposure.Pending;
    public ResearchLine ResearchLine { get; set; }
    public string CongressGuid { get; set; }
    public string Authors { get; set; }
}