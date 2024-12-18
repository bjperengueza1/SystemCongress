using Domain.Entities.Enums;

namespace Application.Users.DTOs;

public class UserInsertDto
{
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public RolerUser Role { get; set; }
}