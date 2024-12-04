using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Author
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AuthorId { get; set; }
    
    public string Name { get; set; }
    
    public string IDNumber { get; set; }
    
    public string InstitutionalMail { get; set; }
    
    public string PersonalMail { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Country { get; set; }
    
    public string City { get; set; }
    
    public AcademicDegree AcademicDegree { get; set; }
}