using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Room
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoomId { get; set; }
    
    public int CongressId { get; set; }
    
    public string Name { get; set; }
    
    public int Capacity { get; set; }
    
    public string Location { get; set; }
    
    // Foreign Key for Congress 
    [ForeignKey("CongressId")]
    public Congress Congress { get; set; }
    
    //one Room has many Exposures
    public virtual ICollection<Exposure> Exposures { get; set; } = new List<Exposure>();
}