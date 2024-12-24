using Application.Rooms.DTOs;
using Application.Rooms.Interfaces;
using Domain.Common.Pagination;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<RoomDto>>> GetRooms([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("El número de página y el tamaño deben ser mayores a 0.");
            }
            
            //var rooms = await _roomService.GetAllAsync();
            var rooms = await _roomService.GetPagedAsync(pageNumber, pageSize);
            
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoom(int id)
        {
            var room = await _roomService.GetByIdAsync(id);
            
            if (room == null) return NotFound();
            
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] RoomInsertDto insertDto)
        {
            var createdRoom = await _roomService.CreateAsync(insertDto);
            
            return CreatedAtAction(nameof(GetRoom), new { id = createdRoom.RoomId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomUpdateDto updateDto)
        {
            var roomDto = await _roomService.UpdateAsync(id, updateDto);
            return roomDto == null ? NotFound() :  Ok(roomDto);
        }
    }
}
