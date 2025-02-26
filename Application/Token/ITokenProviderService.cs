using Domain.Entities;

namespace Application.Token;

public interface ITokenProviderService
{
    string Create(User user);
}