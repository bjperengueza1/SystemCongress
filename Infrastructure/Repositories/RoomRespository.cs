using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class RoomRespository : IRoomRepository
{
    private readonly CongressContext _context;
    
    public RoomRespository(CongressContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _context.Rooms.ToListAsync();
    }

    public async Task<Room> GetByIdAsync(int id)
    {
        return await _context.Rooms.FindAsync(id);
    }

    public async Task AddAsync(Room entity)
    {
        await _context.Rooms.AddAsync(entity);
    }

    public void UpdateAsync(Room entity)
    {
        _context.Rooms.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteAsync(Room entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<PagedResult<Room>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var rooms = await _context.Rooms
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var totalRooms = await _context.Rooms.CountAsync();
        
        return new PagedResult<Room>
        {
            Items = rooms,
            TotalItems = totalRooms,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}