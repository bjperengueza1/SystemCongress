using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CongressContext _context;
    
    public UserRepository(CongressContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<User?>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task AddAsync(User? entity)
    {
        await _context.Users.AddAsync(entity);
    }

    public void UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public Task<PaginatedResult<User>> GetPagedAsync(int pageNumber, int pageSize, string search)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    public async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}