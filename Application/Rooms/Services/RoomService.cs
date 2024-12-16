using Application.Rooms.DTOs;
using Application.Rooms.Interfaces;

namespace Application.Rooms.Services;

public class RoomService : IRoomService
{
    public Task<IEnumerable<RoomDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> CreateAsync(RoomInsertDto ti)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> UpdateAsync(int id, RoomUpdateDto tu)
    {
        throw new NotImplementedException();
    }
}