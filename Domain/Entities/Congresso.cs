using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Congresso
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CongressId { get; set; }
    
    public string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Location { get; set; }
    
    //one Congresso has many Exposures
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

}