namespace Application.Congresses.DTOs;

public class CongressInsertDto
{
    public string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public DateTime EndDateRegistrationExposure { get; set; }
    
    public DateTime EndDateNotificationApprove { get; set; }
    
    public DateTime EndDateRegistrationAttendee { get; set; }
    public string Location { get; set; }
    public int MinHours { get; set; }
}