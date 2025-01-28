using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Enums;

namespace Domain.Entities;

public class Exposure
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExposureId { get; set; }
    
    public string Name { get; set; }
    
    public StatusExposure StatusExposure { get; set; }
    
    public ResearchLine ResearchLine { get; set; }
    
    public string? Guid { get; set; }
    
    // Foreign Key for Room
    public int RoomId { get; set; }
    //set name of the foreign key is RoomId
    /*[ForeignKey("RoomId")]
    public virtual Room? Room { get; set; }*/
    
    // Foreign Key for Congress
    public int CongressId { get; set; }
    //set name of the foreign key is CongressId
    [ForeignKey("CongressId")]
    public Congress? Congress { get; set; }
    
    //exposure can have max 3 authors
    public ICollection<ExposureAuthor> ExposureAuthor { get; set; } = [];
    
    public string SummaryFilePath { get; set; }
    
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    
}