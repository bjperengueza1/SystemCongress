using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Filter;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly CongressContext _context;
    
    public AuthorRepository(CongressContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Author?>> GetAllAsync()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author> GetByIdAsync(int id)
    {
        return await _context.Authors.FindAsync(id);
    }

    public async Task AddAsync(Author? entity)
    {
        await _context.Authors.AddAsync(entity);
    }

    public void UpdateAsync(Author? entity)
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

    public Task<PaginatedResult<Author>> GetPagedAsync(AuthorFilter tf)
    {
        throw new NotImplementedException();
    }

    public async Task<Author?> GetByIdNumberAsync(string idNumber)
    {
        return await _context.Authors.FirstOrDefaultAsync(x => x.IDNumber == idNumber);
    }
    
    //get authors by exposure
    public async Task<IEnumerable<Author>> GetAuthorsByExposureAsync(int exposureId)
    {
        return await _context.ExposureAuthors
            .Where(ea => ea.ExposureId == exposureId)
            .Select(ea => ea.Author)
            .ToListAsync();
    }
    
    //get first author by exposure
    public async Task<Author?> GetFirstAuthorByExposureAsync(int exposureId)
    {
        return await _context.ExposureAuthors
            .Where(ea => ea.ExposureId == exposureId && ea.Position == 0)
            .Select(ea => ea.Author)
            .FirstOrDefaultAsync();
    }
}