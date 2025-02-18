using Application.Attendees.DTOs;
using Application.Attendees.Interfaces;
using Domain.Common.Pagination;
using Domain.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {
        private readonly IAttendeeService _attendeeService;
        
        public AttendeesController(IAttendeeService attendeeService)
        {
            _attendeeService = attendeeService;
        }
        
        //[Autorize(Roles = "Admin")]
        //get by IDNumber
        [Authorize]
        [HttpGet("{idNumber}")]
        public async Task<ActionResult<AttendeeDto>> GetAttendee(string idNumber)
        {
            var attendeeDto = await _attendeeService.GetAttendeeByIdNumberAsync(idNumber);
            
            return attendeeDto == null ? NotFound() : Ok(attendeeDto);
        }
        
        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<AttendeeDto>>> GetAttendees([FromQuery] AttendeeFilter filter)
        {
            if (filter.pageNumber <= 0 || filter.pageSize <= 0)
            {
                return BadRequest("El número de página y el tamaño deben ser mayores a 0.");
            }
            
            var paginatedResult = await _attendeeService.GetPagedAsync(filter);

            return Ok(paginatedResult);
        }
    }
}
