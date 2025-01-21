namespace Application.Attendances.DTOs;
public class AttendanceInsertDto
{
    public DateTime Date { get; set; } = DateTime.Now;
    public int AttendeeId { get; set; }
    public int ExposureId { get; set; }
    
    
}