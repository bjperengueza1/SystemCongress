using Application.Users.DTOs;

namespace Application.Token;

public interface ITokenService
{
    string CreateToken(UserDto user);
}