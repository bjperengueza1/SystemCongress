using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Attendance
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AttendanceId { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Phone { get; set; }
    
    public string Institution { get; set; }
    
    public string IDNumber { get; set; }
    
    // Foreign Key for Exposure
    public int ExposureId { get; set; }
    //set name of the foreign key is ExposureId
    [ForeignKey("ExposureId")]
    public virtual Exposure Exposure { get; set; }
    
    
    
}