using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly CongressContext _context;
    
    public AuthorRepository(CongressContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author> GetByIdAsync(int id)
    {
        return await _context.Authors.FindAsync(id);
    }

    public async Task AddAsync(Author entity)
    {
        await _context.Authors.AddAsync(entity);
    }

    public void UpdateAsync(Author entity)
    {
        _context.Authors.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteAsync(Author entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}