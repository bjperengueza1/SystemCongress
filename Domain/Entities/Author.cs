using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Enums;

namespace Domain.Entities;

public class Author
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AuthorId { get; set; }
    
    //position of the author in the list of authors
    public int Position { get; set; }
    
    public string Name { get; set; }
    
    public string IDNumber { get; set; }
    
    public string InstitutionalMail { get; set; }
    
    public string PersonalMail { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Country { get; set; }
    
    public string City { get; set; }
    
    public AcademicDegree AcademicDegree { get; set; }
    
    // Foreign Key for Exposure
    public int ExposureId { get; set; }
    //set name of the foreign key is ExposureId
    [ForeignKey("ExposureId")]
    public virtual Exposure Exposure { get; set; }
}