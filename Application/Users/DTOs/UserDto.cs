using Domain.Entities.Enums;

namespace Application.Users.DTOs;

public class UserDto
{
    public int UserId { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public RolerUser Role { get; set; }
    
}