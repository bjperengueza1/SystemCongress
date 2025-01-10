using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ExposureRepository : IExposureRepository
{
    private readonly CongressContext _context;
    
    public ExposureRepository(CongressContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Exposure>> GetAllAsync()
    {
        return await _context.Exposures.ToListAsync();
    }

    public async Task<Exposure> GetByIdAsync(int id)
    {
        return await _context.Exposures
            .Include(e => e.Authors)
            .FirstOrDefaultAsync(e => e.ExposureId == id);
    }

    public async Task AddAsync(Exposure entity)
    {
        await _context.Exposures.AddAsync(entity);
        
        //var authors = entity.Authors;
        //recorrer autores e insertarlos en la tabla de autores
        /*foreach (var author in authors)
        {
            var authorEntity = new Authors
            {
                Name = author.Name,
                LastName = author.LastName
            };
            _context.Authors.Add(authorEntity);
        }*/
        
        //throw new NotImplementedException();
    }

    public void UpdateAsync(Exposure entity)
    {
        _context.Exposures.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteAsync(Exposure entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<PaginatedResult<Exposure>> GetPagedAsync(int pageNumber, int pageSize,string search)
    {
        IQueryable<Exposure> query = _context.Exposures
            .Include(e => e.Authors);
        
        if(!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(e => e.Name.Contains(search));
        }
        
        var exposures = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var totalExposures = await query.CountAsync();
        
        return PaginatedResult<Exposure>.Create(exposures, totalExposures, pageNumber, pageSize);
    }

    public Task<PaginatedResult<Exposure>> GetExposuresByRoomPagedAsync(int roomId, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<Exposure>> GetExposuresByCongressPagedAsync(int congressId, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
}