using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class CertificatesAttendance
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CertificatesAttendanceId { get; set; }
    public int CongressId { get; set; }
    public Congress Congress { get; set; }
    public int AttendeeId { get; set; }
    public Attendee Attendee { get; set; }
    public string Guid { get; set; }
}