using Application.Congresses.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Settings;

public class BaseUrlSettings : IBaseUrlSettings
{
    public string BaseUrlFront { get; }
    
    public BaseUrlSettings(IConfiguration configuration)
    {
        BaseUrlFront = configuration["BaseUrlFront"];
    }
}