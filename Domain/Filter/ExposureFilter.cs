using Domain.Entities.Enums;

namespace Domain.Filter;

public class ExposureFilter : Filter
{   
    public int? congressId { get; set; } = 0;
    public int? ResearchLine { get; set; } = 0;
    
    public int? Type { get; set; } = 0;
}