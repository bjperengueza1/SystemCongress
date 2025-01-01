using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CongressRepository : ICongressRepository
{
    
    private readonly CongressContext _context;
    
    public CongressRepository(CongressContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Congress>> GetAllAsync()
    {
        return await _context.Congresses.ToListAsync();
    }

    public async Task<Congress> GetByIdAsync(int id)
    {
        return await _context.Congresses.FindAsync(id);
    }

    public async Task AddAsync(Congress entity)
    {
        await _context.Congresses.AddAsync(entity);
    }

    public void UpdateAsync(Congress entity)
    {
        _context.Congresses.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteAsync(Congress entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<PaginatedResult<Congress>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var congresses = await _context.Congresses
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var totalCongresses = await _context.Congresses.CountAsync();
        
        return new PaginatedResult<Congress>
        {
            Items = congresses,
            TotalItems = totalCongresses,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}