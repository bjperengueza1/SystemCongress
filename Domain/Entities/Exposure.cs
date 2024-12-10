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
    
    // Foreign Key for Congresoo
    public int CongressId { get; set; }
    //set name of the foreign key is CongressId
    [ForeignKey("CongressId")]
    public virtual Congresso Congresso { get; set; }
    
    //exposure can have max 3 authors
    public virtual ICollection<Author> Authors { get; set; }
    
    public string SummaryFilePath { get; set; }
    
}