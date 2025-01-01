namespace Application.Rooms.DTOs;

public class RoomWithCongressDto
{
    public int RoomId { get; set; }
    public int CongressId { get; set; }
    public string CongressName { get; set; } // Nueva propiedad para el nombre del congreso
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string Location { get; set; }
}