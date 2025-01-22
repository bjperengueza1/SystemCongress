using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helpers;
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
        entity.Guid = GuidHelper.GenerateGuid();
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

    public async Task<PaginatedResult<Congress>> GetPagedAsync(int pageNumber, int pageSize, string search)
    {
        IQueryable<Congress> query = _context.Congresses;
        
        if(!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c => c.Name.Contains(search));
        }
        
        //order desc
        query = query.OrderByDescending(c => c.CongressId);
        
        var congresses = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var totalCongresses = await query.CountAsync();

        return PaginatedResult<Congress>.Create(congresses, totalCongresses, pageNumber, pageSize);
        
    }

    public async Task<Congress> GetByGuidAsync(string guid)
    {
        return await _context.Congresses.FirstOrDefaultAsync(c => c.Guid == guid);
    }
}