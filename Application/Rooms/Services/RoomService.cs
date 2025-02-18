using Application.Rooms.DTOs;
using Application.Rooms.Interfaces;
using AutoMapper;
using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Filter;
using Domain.Interfaces;

namespace Application.Rooms.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly ICongressRepository _congressRepository;
    private readonly IMapper _mapper;
    
    public RoomService(
        IRoomRepository roomRepository,
        ICongressRepository congressRepository,
        IMapper mapper)
    {
        _roomRepository = roomRepository;
        _congressRepository = congressRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<RoomDto>> GetAllAsync()
    {
        var rooms = await _roomRepository.GetAllAsync();
        
        return rooms.Select(c => _mapper.Map<RoomDto>(c));
    }

    public async Task<RoomDto> GetByIdAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        
        if (room == null) return null;
        
        return _mapper.Map<RoomDto>(room);
    }

    public async Task<RoomDto> CreateAsync(RoomInsertDto ti)
    {
        var congress = await _congressRepository.GetByIdAsync(ti.CongressId);
        
        if (congress == null) return null;
        
        var room = _mapper.Map<Room>(ti);
        
        await _roomRepository.AddAsync(room);
        await _roomRepository.SaveAsync();
        
        return _mapper.Map<RoomDto>(room);
    }

    public async Task<RoomDto> UpdateAsync(int id, RoomUpdateDto tu)
    {
        //Traigo el objeto
        var room = await _roomRepository.GetByIdAsync(id);
        
        if (room == null) return null;
        
        //Y lo que coincida lo actualizo
        room = _mapper.Map(tu, room);
        
        _roomRepository.UpdateAsync(room);
        await _roomRepository.SaveAsync();
        
        return _mapper.Map<RoomDto>(room);
    }
    
    public async Task<PaginatedResult<RoomDto>> GetPagedAsync(RoomFilter tf)
    {
        var paginatedData = await _roomRepository.GetPagedAsync(tf);
        
        return paginatedData.Map(c => _mapper.Map<RoomDto>(c));
    }

    public async Task<PaginatedResult<RoomDto>> GetRoomsByCongressPagedAsync(int congressId, int pageNumber, int pageSize)
    {
        var pagedData = await _roomRepository.GetRoomsByCongressPagedAsync(congressId, pageNumber, pageSize);
        
        return pagedData.Map(c => _mapper.Map<RoomDto>(c));
    }

    public async Task<PaginatedResult<RoomWithCongressDto>> GetRoomsWithCongressPagedAsync(int pageNumber, int pageSize, string search)
    {
        var pagedData = await _roomRepository.GetRoomsWithCongressPagedAsync(pageNumber, pageSize, search);
        
        return pagedData.Map(c => _mapper.Map<RoomWithCongressDto>(c));
    }
}