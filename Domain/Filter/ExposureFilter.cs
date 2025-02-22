using Domain.Entities.Enums;

namespace Domain.Filter;

public class ExposureFilter : Filter
{   
    public int? congressId { get; set; }
    public int? ResearchLine { get; set; } = 0;
}