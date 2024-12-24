namespace Application.Rooms.DTOs;

public class RoomInsertDto
{
    public int CongressId { get; set; }
    
    public string Name { get; set; }
    
    public int Capacity { get; set; }
    
    public string Location { get; set; }
}