namespace Application.Utils.Filter;

public class FilterDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string Search { get; set; } = "";
}