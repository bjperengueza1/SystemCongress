using Domain.Common.Pagination;
using Domain.Entities;
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

    public Task<PaginatedResult<Attendee>> GetPagedAsync(int pageNumber, int pageSize, string search)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Attendee> GetAttendeeByIdNumberAsync(string idNumber)
    {
        return await _context.Attendees.FirstOrDefaultAsync(a => a.IDNumber == idNumber);
    }
}