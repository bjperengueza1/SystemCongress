namespace Application.Congresses.DTOs;

public class CongressDto
{
    public int CongressId { get; set; }
    
    public string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Location { get; set; }
    
    public int MinHours { get; set; }
    
    public string Guid { get; set; }
}