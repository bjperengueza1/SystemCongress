using Domain.Entities.Enums;

namespace Domain.Dtos;

public class CongressCertificate
{
    public int CongressId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public IEnumerable<ExposureWithOutRelationsDto> Exposure { get; set; }
    public bool CertificateAttendance { get; set; } = false;
}

public class ExposureWithOutRelationsDto
{
    public int ExposureId { get; set; }
    public string Name { get; set; }
    public StatusExposure StatusExposure { get; set; }
    public ResearchLine ResearchLine { get; set; }
    public TypeExposure Type { get; set; }
    public DateTime Date { get; set; }
    public string? Guid { get; set; }
}