using Application.Authors.DTOs;
using Domain.Entities.Enums;

namespace Application.Exposures.DTOs;

public class ExposureInsertDto
{
    public string NameExposure { get; set; }
    //StatusExposure by default is "Pending"
    private StatusExposure StatusExposure { get; set; }
    public ResearchLine ResearchLine { get; set; }
    public int CongressId { get; set; }
    public List<AuthorInsertDto> Authors { get; set; }
    
    public string SummaryFilePath { get; set; }
}