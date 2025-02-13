using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Congress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CongressId { get; set; }
    
    public string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Location { get; set; }
    
    //status active or inactive
    public bool Status { get; set; }
    
    //Minimo de horas completadas parar recibir certificado
    public int MinHours { get; set; }
    
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    
    //string guuid for the congress
    public string? Guid { get; set; }
    
    public string? fileFlayer { get; set; } = string.Empty;
    
    public string? fileCertificateConference { get; set; } = string.Empty;
    
    public string? fileCertificateAttendance { get; set; } = string.Empty;
    
    public string? fileCertificateExposure { get; set; } = string.Empty;

}