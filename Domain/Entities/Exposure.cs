using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Exposure
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExposureId { get; set; }
    
    public string Name { get; set; }
    
    public ResearchLine ResearchLine { get; set; }
    
    
    
    
    
}