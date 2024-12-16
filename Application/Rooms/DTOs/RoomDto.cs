namespace Application.Rooms.DTOs;

public class RoomDto
{
    public int RoomId { get; set; }
    
    public int CongressoId { get; set; }
    
    public string Name { get; set; }
    
    public int Capacity { get; set; }
    
    public string Location { get; set; }
}