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
}