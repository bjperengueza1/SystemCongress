namespace Application.Password;

public interface IPasswordHasher
{
    (byte[] PasswordHash, byte[] PasswordSalt) CreatePasswordHash(string password);
    bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
}