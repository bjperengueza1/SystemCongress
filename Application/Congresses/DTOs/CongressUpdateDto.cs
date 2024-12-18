namespace Application.Congresses.DTOs;

public class CongressUpdateDto
{
    //No se necesita el ID en el DTO de actualización
    //public int CongressID { get; set; }
    
    public string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Location { get; set; }
}