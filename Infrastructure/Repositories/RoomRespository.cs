using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Domain.Repositories;

public class RoomRespository : IRoomRepository
{
    private readonly CongressContext _context;
    
    public RoomRespository(CongressContext context)
    {
        _context = context;
    }
    
    public Task<IEnumerable<Room>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Room> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Room entity)
    {
        throw new NotImplementedException();
    }

    public void UpdateAsync(Room entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(Room entity)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}