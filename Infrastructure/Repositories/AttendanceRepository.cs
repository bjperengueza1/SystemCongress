using Domain.Common.Pagination;
using Domain.Entities;
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

    public async Task<PaginatedResult<Attendance>> GetPagedAsync(int pageNumber, int pageSize, string search)
    {
        IQueryable<Attendance> query = _context.Attendances
            .Include(e => e.Attendee)
            .Include(e => e.Exposure);
        
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(
                e => e.Attendee.Name.Contains(search) || 
                     e.Exposure.Name.Contains(search));
        }
        
        //order desc
        query = query.OrderByDescending(e => e.AttendanceId);
        
        var attendances = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var totalItems = await query.CountAsync();
        
        return PaginatedResult<Attendance>.Create(attendances, totalItems, pageNumber, pageSize);
    }

    public async Task<Attendance> GetByAttendeeIdAndExposureIdAsync(int attendeeId, int exposureId)
    {
        return await _context.Attendances
            .FirstOrDefaultAsync(e => e.AttendeeId == attendeeId && e.ExposureId == exposureId);
    }
}