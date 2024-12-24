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
    
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

}