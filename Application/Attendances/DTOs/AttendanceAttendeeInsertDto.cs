using Application.Attendees.DTOs;

namespace Application.Attendances.DTOs;

public class AttendanceAttendeeInsertDto
{
    public int ExposureId { get; set; }
    
    public AttendeeDto Attendee { get; set; }
    
}