namespace Domain.Filter;

public class ExposureFilter : Filter
{
    public int pageNumber { get; set; }
    public int pageSize { get; set; }
    public string search { get; set; }
    
    public int? CongressId { get; set; }
}