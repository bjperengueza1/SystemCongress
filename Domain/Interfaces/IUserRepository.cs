using Domain.Entities;
using Domain.Filter;

namespace Domain.Interfaces;

public interface IUserRepository : IRepository<User, UserFilter>
{
    Task<User?> GetByEmail(string email);
    Task<bool> UserExists(string email);
}