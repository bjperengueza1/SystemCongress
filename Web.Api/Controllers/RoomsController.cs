using Application.Rooms.DTOs;
using Application.Rooms.Interfaces;
using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginatedResult<RoomDto>>> GetRooms([FromQuery] RoomFilter filter)
        {
            if (filter.pageNumber <= 0 || filter.pageSize <= 0)
            {
                return BadRequest("El número de página y el tamaño deben ser mayores a 0.");
            }
            
            var rooms = await _roomService.GetPagedAsync(filter);
            
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<RoomDto>> GetRoom(int id)
        {
            var room = await _roomService.GetByIdAsync(id);
            
            if (room == null) return NotFound();
            
            return Ok(room);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRoom([FromBody] RoomInsertDto insertDto)
        {
            var createdRoom = await _roomService.CreateAsync(insertDto);
            
            return CreatedAtAction(nameof(GetRoom), new { id = createdRoom.RoomId }, null);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomUpdateDto updateDto)
        {
            var roomDto = await _roomService.UpdateAsync(id, updateDto);
            return roomDto == null ? NotFound() :  Ok(roomDto);
        }
        
        //obtener las salas de un congreso
        [HttpGet("/api/Congress/{congressId:int}/Rooms")]
        [Authorize]
        public async Task<ActionResult<PaginatedResult<RoomDto>>> GetRoomsByCongress(int congressId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("El número de página y el tamaño deben ser mayores a 0.");
            }
            
            var rooms = await _roomService.GetRoomsByCongressPagedAsync(congressId, pageNumber, pageSize);

            return Ok(rooms);
        }
        
        //obtener las salas con el congreso {RoomId,CongressId,CongressName,Name,Capacity,Location}
        [HttpGet("WithCongress")]
        [Authorize]
        public async Task<ActionResult<PaginatedResult<RoomWithCongressDto>>> GetRoomsWithCongress([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("El número de página y el tamaño deben ser mayores a 0.");
            }

            var rooms = await _roomService.GetRoomsWithCongressPagedAsync(pageNumber, pageSize, search);
            
            return Ok(rooms);
        }
        
    }
}
