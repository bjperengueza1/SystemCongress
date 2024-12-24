using Application.Token;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Settings;

public class TokenSettings : ITokenSettings
{
    public string TokenKey { get; }
    
    public TokenSettings(IConfiguration configuration)
    {
        TokenKey = configuration["TokenKey"];
    }
}