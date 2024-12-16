using Application.Common;
using Application.Rooms.DTOs;

namespace Application.Rooms.Interfaces;

public interface IRoomService : ICommonService<RoomDto, RoomInsertDto, RoomUpdateDto>
{
    
}