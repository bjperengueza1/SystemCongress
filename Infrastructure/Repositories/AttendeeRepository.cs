using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Filter;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AttendeeRepository : IAttendeeRepository
{
    private readonly CongressContext _context;
    
    public AttendeeRepository(CongressContext context)
    {
        _context = context;
    }
    public Task<IEnumerable<Attendee>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Attendee> GetByIdAsync(int id)
    {
        return await _context.Attendees.FindAsync(id);
    }

    public async Task AddAsync(Attendee entity)
    {
        await _context.Attendees.AddAsync(entity);
    }

    public void UpdateAsync(Attendee entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(Attendee entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public async Task<PaginatedResult<Attendee>> GetPagedAsync(AttendeeFilter tf)
    {
        IQueryable<Attendee> query = _context.Attendees;
        
        if (!string.IsNullOrEmpty(tf.search))
        {
            query = query.Where(
                e => e.Name.Contains(tf.search) || 
                     e.IDNumber.Contains(tf.search) ||
                     e.Institution.Contains(tf.search));
        }
        
        //order desc
        query = query.OrderByDescending(e => e.AttendeeId);
        
        var attendees = await query
            .Skip((tf.pageNumber - 1) * tf.pageSize)
            .Take(tf.pageSize)
            .ToListAsync();
        
        var totalItems = await query.CountAsync();
        
        return PaginatedResult<Attendee>.Create(attendees, totalItems, tf.pageNumber, tf.pageSize);
        
    }

    public async Task<Attendee> GetAttendeeByIdNumberAsync(string idNumber)
    {
        return await _context.Attendees.FirstOrDefaultAsync(a => a.IDNumber == idNumber);
    }

    public async Task<string> GetGuidCertificateAttendanceAsync(int congressId, int attendeeId)
    {
        var c = await _context.CertificatesAttendances.FirstAsync(c => c.CongressId == congressId && c.AttendeeId == attendeeId);
        return c.Guid;
    }
}