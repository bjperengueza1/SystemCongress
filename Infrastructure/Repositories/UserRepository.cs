using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Filter;
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
        _context.Users.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
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

    public async Task<PaginatedResult<User>> GetPagedAsync(UserFilter tf)
    {
        IQueryable<User> query = _context.Users;
        
        if(!string.IsNullOrWhiteSpace(tf.search))
        {
            query = query.Where(c => c.Name.Contains(tf.search) || c.Email.Contains(tf.search));
        }
        
        //var queryResult = query.AsNoTracking();
        query = query.OrderByDescending(c => c.UserId);
        
        var users = await query
            .Skip((tf.pageNumber - 1) * tf.pageSize)
            .Take(tf.pageSize)
            .ToListAsync();
        
        var totalItems = await query.CountAsync();
        
        return PaginatedResult<User>.Create(users, totalItems, tf.pageNumber, tf.pageSize);
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