using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Filter;


namespace Domain.Interfaces;

public interface IRoomRepository : IRepository<Room, RoomFilter>
{
    Task<PaginatedResult<Room>> GetRoomsByCongressPagedAsync(int congressId, int pageNumber, int pageSize);
    Task<PaginatedResult<Room>> GetRoomsWithCongressPagedAsync(int pageNumber, int pageSize, string search);
    //Task<List<(Room room, string congressName)>> GetRoomsWithCongressPagedAsync();
    //Task<PaginatedResult<(Room, string congresName)>> GetRoomsWithCongressPagedAsync(int pageNumber, int pageSize);
}