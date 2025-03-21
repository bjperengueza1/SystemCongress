using Application.Rooms.DTOs;
using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Filter;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

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

    public async Task<PaginatedResult<Room>> GetPagedAsync(RoomFilter tf)
    {
        IQueryable<Room> query = _context.Rooms;
        
        if (!string.IsNullOrWhiteSpace(tf.search))
        {
            query = query.Where(r => r.Name.Contains(tf.search));
        }
        
        //order desc
        query = query.OrderByDescending(r => r.RoomId);
        
        var rooms = await query
            .Skip((tf.pageNumber - 1) * tf.pageSize)
            .Take(tf.pageSize)
            .ToListAsync();
        
        var totalRooms = await query.CountAsync();
        return PaginatedResult<Room>.Create(rooms, totalRooms, tf.pageNumber, tf.pageSize);
    }

    public async Task<PaginatedResult<Room>> GetRoomsByCongressPagedAsync(int congressId, int pageNumber, int pageSize)
    {
        var query = _context.Rooms
            .Where(r => r.CongressId == congressId);
        
        var rooms = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var totalRooms = await query.CountAsync();
        
        return PaginatedResult<Room>.Create(rooms, totalRooms, pageNumber, pageSize);
    }


    
    public async Task<PaginatedResult<Room>> GetRoomsWithCongressPagedAsync(int pageNumber, int pageSize, string search)
    {
        
        IQueryable<Room> query = _context.Rooms
            .Include(r => r.Congress);

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(r => r.Name.Contains(search) || r.Congress.Name.Contains(search));
        }
        
        var rooms = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var totalRooms = await query.CountAsync();
        
        return PaginatedResult<Room>.Create(rooms, totalRooms, pageNumber, pageSize);
    }
}


