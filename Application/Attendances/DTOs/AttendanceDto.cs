using Application.Attendees.DTOs;
using Application.Exposures.DTOs;
using Domain.Entities;

namespace Application.Attendances.DTOs;
public class AttendanceDto
{
    
    public int AttendanceId { get; set; }
    
    public DateTime Date { get; set; }
    
    public AttendeeDto Attendee { get; set; }
    
    public ExposureWitchAuthorsDto Exposure { get; set; }
}