using Application.Congresses.Interfaces;

namespace Application.Congresses.Services;

public class RegistrationUrlService : IRegistrationUrlService
{
    private readonly string _baseUrlFront;

    public RegistrationUrlService(IBaseUrlSettings urlSettings)
    {
        _baseUrlFront = urlSettings.BaseUrlFront;
    }
    
    public string GenerateRegistrationUrl(string guid)
    {
        return $"{_baseUrlFront}/registration/{guid}";
    }
}