namespace Application.Users.DTOs;

public class UserChangePasswordByAdminDto
{
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}