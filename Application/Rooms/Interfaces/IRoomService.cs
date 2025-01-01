using Application.Common;
using Application.Rooms.DTOs;
using Domain.Common.Pagination;

namespace Application.Rooms.Interfaces;

public interface IRoomService : ICommonService<RoomDto, RoomInsertDto, RoomUpdateDto>
{
    Task<PaginatedResult<RoomDto>> GetRoomsByCongressPagedAsync(int congressId, int pageNumber, int pageSize);
    Task<PaginatedResult<RoomWithCongressDto>> GetRoomsWithCongressPagedAsync(int pageNumber, int pageSize);
}