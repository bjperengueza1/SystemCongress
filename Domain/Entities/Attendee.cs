namespace Domain.Entities;

public class Attendee
{
    public int AttendeeId { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Phone { get; set; }
    
    public string Institution { get; set; }
    
    public string IDNumber { get; set; }
    
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
}