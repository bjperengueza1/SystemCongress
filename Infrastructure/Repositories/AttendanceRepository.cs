using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Filter;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly CongressContext _context;
    
    public AttendanceRepository(CongressContext context)
    {
        _context = context;
    }
    
    public Task<IEnumerable<Attendance>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Attendance> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Attendance entity)
    {
        await _context.Attendances.AddAsync(entity);
    }

    public void UpdateAsync(Attendance entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(Attendance entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<PaginatedResult<Attendance>> GetPagedAsync(AttendanceFilter tf)
    {
        IQueryable<Attendance> query = _context.Attendances
            .Include(e => e.Attendee)
            .Include(e => e.Exposure)
            .ThenInclude(e => e.Congress);
        
        if (!string.IsNullOrEmpty(tf.search))
        {
            query = query.Where(
                e => e.Attendee.Name.Contains(tf.search) || 
                     e.Exposure.Name.Contains(tf.search));
        }
        
        if(tf.congressId is > 0)
        {
            query = query.Where(e => e.Exposure.CongressId == tf.congressId.Value);
        }
        
        //order desc
        query = query.OrderByDescending(e => e.AttendanceId);
        
        var attendances = await query
            .Skip((tf.pageNumber - 1) * tf.pageSize)
            .Take(tf.pageSize)
            .ToListAsync();
        
        var totalItems = await query.CountAsync();
        
        return PaginatedResult<Attendance>.Create(attendances, totalItems, tf.pageNumber, tf.pageSize);
    }

    public async Task<Attendance> GetByAttendeeIdAndExposureIdAsync(int attendeeId, int exposureId)
    {
        return await _context.Attendances
            .FirstOrDefaultAsync(e => e.AttendeeId == attendeeId && e.ExposureId == exposureId);
    }

    public async Task<IEnumerable<Attendance>> GetAllEAsync(AttendanceFilter tf)
    {
        IQueryable<Attendance> query = _context.Attendances
            .Include(e => e.Attendee)
            .Include(e => e.Exposure)
            .ThenInclude(e => e.Congress);
        if (!string.IsNullOrEmpty(tf.search))
        {
            query = query.Where(
                e => e.Attendee.Name.Contains(tf.search) || 
                     e.Exposure.Name.Contains(tf.search));
        }
        
        if(tf.congressId is > 0)
        {
            query = query.Where(e => e.Exposure.CongressId == tf.congressId.Value);
        }
        
        //order desc
        query = query.OrderByDescending(e => e.AttendanceId);
        
        var attendances = await query.ToListAsync();
        
        return attendances;
    }
}