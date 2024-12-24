using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Users.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace Application.Token;

public class TokenService : ITokenService
{
    public readonly SymmetricSecurityKey _key;

    public TokenService(ITokenSettings tokenSettings)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.TokenKey));
    }
    public string CreateToken(UserDto user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString())
        };
        
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}