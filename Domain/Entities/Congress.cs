namespace Domain.Entities;

public class Congress
{
    public int CongressID { get; set; }
    
    public string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Location { get; set; }
    
}