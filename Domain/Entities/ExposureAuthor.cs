using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ExposureAuthor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExposureAuthorId { get; set; }
    
    //position of the author in the list of authors
    public int Position { get; set; }
    
    public int ExposureId { get; set; }
    [ForeignKey("ExposureId")]
    public Exposure Exposure { get; set; } = null!;
    
    public int AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public Author Author { get; set; } = null!;
}