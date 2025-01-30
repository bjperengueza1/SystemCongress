namespace Domain.Entities;

public class CongressCertificate
{
    public int CongressId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public IEnumerable<Exposure> Exposures { get; set; } = [];
    public bool CertificateAttendance { get; set; } = false;
}