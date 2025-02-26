using Domain.Entities;

namespace Domain.Interfaces.Token;

public interface ITokenProvider
{
    string Create(User user);
}