namespace Application.Congresses.DTOs;

public class CertificateAttendanceDto
{
    public int CertificatesAttendanceId { get; set; }
    public string Guid { get; set; }
    public string AttendeeName { get; set; }
    public string AttendeeEmail { get; set; }
    public string AttendeeIDNumber { get; set; }
    public string CongressName { get; set; }
    public string CongressStartDate { get; set; }
    public string CongressEndDate { get; set; }
}