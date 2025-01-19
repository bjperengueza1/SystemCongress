using Application.Authors.DTOs;
using Domain.Entities.Enums;

namespace Application.Exposures.DTOs;

public class ExposureWitchAuthorsDto
{
    public int ExposureId { get; set; }
    
    public string Name { get; set; }
    
    public StatusExposure StatusExposure { get; set; }
    
    public ResearchLine ResearchLine { get; set; }
    
    public string SummaryFilePath { get; set; }
    
    public int CongressId { get; set; }
    
    public ICollection<AuthorDto> Authors { get; set; }
    
    
}