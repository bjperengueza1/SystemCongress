using Application.Password;
using Application.Token;
using Application.Users.DTOs;
using Application.Users.Interfaces;
using AutoMapper;
using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Filter;
using Domain.Interfaces;
using Domain.Interfaces.Token;

namespace Application.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly ITokenProviderService _tokenProvider;
    
    public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher, ITokenProviderService tokenProvider)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
    }
    
    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        return _userRepository.GetAllAsync().Result.Select(u => _mapper.Map<UserDto>(u));
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateAsync(UserInsertDto ti)
    {
        var (passwordHash, passwordSalt) = _passwordHasher.CreatePasswordHash(ti.Password);
        
        var user = _mapper.Map<User>(ti);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
        
        return _mapper.Map<UserDto>(user);
    }

    public Task<UserDto> UpdateAsync(int id, UserUpdateDto tu)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<UserDto>> GetPagedAsync(int pageNumber, int pageSize, string search)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedResult<UserDto>> GetPagedAsync(UserFilter tf)
    {
        var pagedData = await _userRepository.GetPagedAsync(tf);
        return pagedData.Map(u => _mapper.Map<UserDto>(u));
    }

    public async Task<UserDto?> GetByEmail(string email)
    {
        var user = await _userRepository.GetByEmail(email);
        return _mapper.Map<UserDto>(user);
    }
    
    public async Task<bool> UserExists(string email)
    {
        return await _userRepository.UserExists(email);
    }
    
    public async Task<UserLoggedDto?> Authenticate(UserLoginDto loginDto)
    {
        var user = await _userRepository.GetByEmail(loginDto.Email);
        
        if (user == null) return null;
        
        if (!_passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash, user.PasswordSalt))
        {
            return null;
        }
        
        var userLoggedDto = _mapper.Map<UserLoggedDto>(user);
        
        userLoggedDto.Token = _tokenProvider.Create(user);
        
        
        //userLoggedDto.Token = _tokenService.CreateToken(userLoggedDto);

        return userLoggedDto;
    }
    
}