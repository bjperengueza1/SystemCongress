using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Domain.Interfaces.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Infrastructure.Token;

public class TokenProvider(IConfiguration configuration) : ITokenProvider 
{
    public string Create(User user)
    {
        string secretKey = configuration["JWT:Secret"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new (JwtRegisteredClaimNames.NameId, user.UserId.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials,
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"]
        };
        
        var tokenHandler = new JsonWebTokenHandler();
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return token;
    }
}