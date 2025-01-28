using Application.Authors.DTOs;
using Domain.Entities.Enums;

namespace Application.Exposures.DTOs;

public class ExposureInsertDto
{
    public string Name { get; set; }
    //StatusExposure by default is "Pending"
    private StatusExposure Status { get; set; } = StatusExposure.Pending;
    public ResearchLine ResearchLine { get; set; }
    public int CongressId { get; set; }
    public List<AuthorInsertDto> Authors { get; set; }
    
    public string SummaryFilePath { get; set; }
}