namespace Application.Congresses.DTOs;

public class CongressInsertDto
{
    public string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Location { get; set; }
    public int MinHours { get; set; }
}