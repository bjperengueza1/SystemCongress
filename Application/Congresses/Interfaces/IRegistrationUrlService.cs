namespace Application.Congresses.Interfaces;

public interface IRegistrationUrlService
{
    string GenerateRegistrationUrl(string guid);
}