using Domain.Entities.Enums;

namespace Application.Authors.DTOs;

public class AuthorDto
{
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
}