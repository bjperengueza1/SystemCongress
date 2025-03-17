using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Filter;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helpers;
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
            .Include(e => e.Congress)
            .FirstOrDefaultAsync(e => e.ExposureId == id);
    }

    public async Task AddAsync(Exposure entity)
    {
        entity.Guid = GuidHelper.GenerateGuid();
        entity.GuidCert = Guid.NewGuid().ToString();
        await _context.Exposures.AddAsync(entity);
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
    public async Task<PaginatedResult<Exposure>> GetPagedAsync(ExposureFilter tf)
    {
        IQueryable<Exposure> query = _context.Exposures
            .Include(e => e.Congress)
            .Include(e => e.ExposureAuthor)
            .ThenInclude(ea => ea.Author);

        if(!string.IsNullOrWhiteSpace(tf.search))
        {
            query = query.Where(e => e.Name.Contains(tf.search) || e.GuidCert.Contains(tf.search));
        }

        if(tf.congressId.HasValue && tf.congressId != 0)
        {
            query = query.Where(e => e.CongressId == tf.congressId);
        }
        
        if(tf.ResearchLine.HasValue && tf.ResearchLine != 0)
        {
            query = query.Where(e => e.ResearchLine == (ResearchLine)tf.ResearchLine);
        }

        if (tf.Type.HasValue && tf.Type != 0)
        {
            query = query.Where(e => e.Type == (TypeExposure)tf.Type);
        }
        
        //order desc
        query = query.OrderByDescending(e => e.ExposureId);
        
        var exposures = await query
            .Skip((tf.pageNumber - 1) * tf.pageSize)
            .Take(tf.pageSize)
            .ToListAsync();
        
        var totalExposures = await query.CountAsync();
        
        return PaginatedResult<Exposure>.Create(exposures, totalExposures, tf.pageNumber, tf.pageSize);
    }

    public async Task<PaginatedResult<Exposure>> GetPagedWitchRoomsAsync(int pageNumber, int pageSize, string search)
    {
        IQueryable<Exposure> query = _context.Exposures
            .Include(e => e.Room)
            .Include(e  => e.Congress)
            .Include(e => e.ExposureAuthor)
            .ThenInclude(ea => ea.Author);

        if(!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(e => e.Name.Contains(search));
        }
        
        //order desc
        query = query.OrderByDescending(e => e.ExposureId);
        
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

    public async Task<PaginatedResult<Exposure>> GetExposuresByCongressPagedAsync(int congressId, int pageNumber, int pageSize)
    {
        IQueryable<Exposure> query = _context.Exposures
            .Include(e => e.Room)
            .Include(e => e.Congress)
            .Include(e => e.ExposureAuthor)
            .ThenInclude(ea => ea.Author);
        
        query = query.Where(e => e.CongressId == congressId);
        
        var exposures = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var totalExposures = await query.CountAsync();
        
        return PaginatedResult<Exposure>.Create(exposures, totalExposures, pageNumber, pageSize);
    }
    
    public async Task<PaginatedResult<Exposure>> GetExposuresApprovedByCongressPagedAsync(int congressId, int pageNumber, int pageSize)
    {
        IQueryable<Exposure> query = _context.Exposures
            .Include(e => e.Room)
            .Include(e => e.Congress)
            .Include(e => e.ExposureAuthor)
            .ThenInclude(ea => ea.Author);
        
        query = query.Where(e => e.CongressId == congressId && e.StatusExposure == StatusExposure.Approved);
        
        var exposures = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var totalExposures = await query.CountAsync();
        
        return PaginatedResult<Exposure>.Create(exposures, totalExposures, pageNumber, pageSize);
    }

    public async Task<Exposure> GetByGuidAsync(string guid)
    {
        IQueryable<Exposure> query = _context.Exposures
            .Include(e => e.ExposureAuthor)
            .ThenInclude(ea => ea.Author);
        
        query = query.Where(e => e.Guid == guid);
        
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Exposure>> GetAllEAsync(ExposureFilter tf)
    {
        IQueryable<Exposure> query = _context.Exposures
            .Include(e => e.Congress)
            .Include(e => e.ExposureAuthor)
            .ThenInclude(ea => ea.Author);

        if(!string.IsNullOrWhiteSpace(tf.search))
        {
            query = query.Where(e => e.Name.Contains(tf.search));
        }

        if(tf.congressId.HasValue && tf.congressId != 0)
        {
            query = query.Where(e => e.CongressId == tf.congressId);
        }
        
        if(tf.ResearchLine.HasValue && tf.ResearchLine != 0)
        {
            query = query.Where(e => e.ResearchLine == (ResearchLine)tf.ResearchLine);
        }
        
        if (tf.Type.HasValue && tf.Type != 0)
        {
            query = query.Where(e => e.Type == (TypeExposure)tf.Type);
        }
        
        //order desc
        query = query.OrderByDescending(e => e.ExposureId);
        
        var exposures = await query.ToListAsync();
        
        return exposures;
    }
}