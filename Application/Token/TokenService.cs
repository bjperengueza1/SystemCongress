using System.Text;
using Application.Users.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace Application.Token;

public class TokenService : ITokenService
{
    public readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration configuration)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
    }
    public string CreateToken(UserDto user)
    {
        throw new NotImplementedException();
    }
}