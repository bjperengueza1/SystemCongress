using Domain.Entities;
using Domain.Interfaces.Token;

namespace Application.Token;

public class TokenProviderService : ITokenProviderService
{
    private readonly ITokenProvider _tokenProvider;
    
    public TokenProviderService(ITokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }
    public string Create(User user)
    {
        return _tokenProvider.Create(user);
    }
}