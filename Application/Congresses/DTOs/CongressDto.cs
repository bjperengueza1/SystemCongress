namespace Application.Congresses.DTOs;

public class CongressDto
{
    public int CongressId { get; set; }
    
    public string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public DateTime EndDateRegistrationExposure { get; set; }
    
    public DateTime EndDateNotificationApprove { get; set; }
    
    public DateTime EndDateRegistrationAttendee { get; set; }
    public string Location { get; set; }
    
    public bool Status { get; set; } = false;
    
    public int MinHours { get; set; }
    public string Guid { get; set; }
    
    public string? fileFlayer { get; set; } = string.Empty;
    
    public string? fileCertificateConference { get; set; } = string.Empty;
    
    public string? fileCertificateAttendance { get; set; } = string.Empty;
    
    public string? fileCertificateExposure { get; set; } = string.Empty;
}