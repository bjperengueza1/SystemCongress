using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Attendance
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AttendanceId { get; set; }
    
    public DateTime Date { get; set; }
    
    public int AttendeeId { get; set; }
    // Foreign Key for Attendee
    [ForeignKey("AttendeeId")]
    public virtual Attendee Attendee { get; set; }
    
    // Foreign Key for Exposure
    public int ExposureId { get; set; }
    //set name of the foreign key is ExposureId
    [ForeignKey("ExposureId")]
    public virtual Exposure Exposure { get; set; }
}