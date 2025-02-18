namespace Domain.Filter;

public class Filter
{
    public int pageNumber { get; set; } = 1;
    public int pageSize { get; set; } = 10;
    public string search { get; set; } = "";
}