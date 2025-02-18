using Application.Common;
using Application.Users.DTOs;
using Domain.Filter;

namespace Application.Users.Interfaces;

public interface IUserService : ICommonService<UserDto, UserInsertDto, UserUpdateDto, UserFilter>
{
    //Task<UserDto> Authenticate(string email, string password);
    /*Task<UserDto> RefreshToken(string token, string refreshToken);
    Task<bool> RevokeToken(string token);
    Task<bool> VerifyEmail(string token);
    Task<bool> ForgotPassword(string email);
    Task<bool> ResetPassword(string token, string password);
    Task<bool> ChangePassword(int userId, string currentPassword, string newPassword);
    Task<bool> UpdateRole(int userId, int roleId);
    Task<bool> UpdateToken(int userId, string token);
    Task<bool> UpdateRefreshToken(int userId, string refreshToken);
    Task<bool> UpdateRefreshTokenExpiryTime(int userId, DateTime refreshTokenExpiryTime);
    Task<bool> UpdateIsVerified(int userId, bool isVerified);
    Task<UserDto> GetByEmail(string email);
    Task<UserDto> GetByToken(string token);
    Task<UserDto> GetByRefreshToken(string refreshToken);
    Task<UserDto> GetByRefreshTokenExpiryTime(DateTime refreshTokenExpiryTime);
    Task<UserDto> GetByIsVerified(bool isVerified);
    Task<UserDto> GetByRole(int roleId);
    Task<UserDto> GetByTokenAndRefreshToken(string token, string refreshToken);
    Task<UserDto> GetByEmailAndPassword(string email, string password);
    Task<UserDto> GetByEmailAndToken(string email, string token);
    Task<UserDto> GetByEmailAndRefreshToken(string email, string refreshToken);
    Task<UserDto> GetByEmailAndRefreshTokenExpiryTime(string email, DateTime refreshTokenExpiryTime);
    Task<UserDto> GetByEmailAndIsVerified(string email, bool isVerified);
    Task<UserDto> GetByEmailAndRole(string email, int roleId);
    Task<UserDto> GetByEmailAndTokenAndRefreshToken(string email, string token, string refreshToken);
    Task<UserDto> GetByEmailAndPasswordAndToken(string email, string password, string token);
    Task<UserDto> GetByEmailAndPasswordAndRefreshToken(string email, string password, string refreshToken);
    Task<UserDto> GetByEmailAndPasswordAndRefreshTokenExpiryTime(string email, string password, DateTime refreshTokenExpiryTime);
    Task<UserDto> GetByEmailAndPasswordAndIsVerified(string email, string password, bool isVerified);
    Task<UserDto> GetByEmailAndPasswordAndRole(string email, string password, int roleId);
    Task<UserDto> GetByEmailAndPasswordAndTokenAndRefreshToken(string email, string password, string token, string refreshToken);
    */
    
    Task<UserDto?> GetByEmail(string email);
    Task<bool> UserExists(string email);
    Task<UserLoggedDto?> Authenticate(UserLoginDto loginDto);
    
}