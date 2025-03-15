using Application.Attendances.Interfaces;
using Application.Attendances.DTOs;
using Application.Attendees.DTOs;
using Application.Attendees.Interfaces;
using AutoMapper;
using Domain.Common.Pagination;
using Domain.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IAttendeeService _attendeeService;
        private readonly IMapper _mapper;
        
        public AttendancesController(
            IAttendanceService attendanceService,
            IAttendeeService attendeeService,
            IMapper mapper)
        {
            _attendanceService = attendanceService;
            _attendeeService = attendeeService;
            _mapper = mapper;
        }
        
        /*private readonly IAttendanceService _attendanceService;

        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AttendanceDto>> CreateAttendance(AttendanceInsertDto attendanceInsertDto)
        {
            var attendanceDto = await _attendanceService.CreateAttendanceAsync(attendanceInsertDto);

            return CreatedAtAction(nameof(GetAttendance), new { id = attendanceDto.Id }, attendanceDto);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceDto>> GetAttendance(int id)
        {
            var attendanceDto = await _attendanceService.GetAttendanceByIdAsync(id);

            return attendanceDto == null ? NotFound() : Ok(attendanceDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<AttendanceDto>> UpdateAttendance(int id, AttendanceUpdateDto attendanceUpdateDto)
        {
            var attendanceDto = await _attendanceService.UpdateAttendanceAsync(id, attendanceUpdateDto);

            return attendanceDto == null ? NotFound() : Ok(attendanceDto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttendance(int id)
        {
            var result = await _attendanceService.DeleteAttendanceAsync(id);

            return result ? NoContent() : NotFound();
        }*/

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<AttendanceDto>>> GetAttendances([FromQuery] AttendanceFilter filter)
        {
            var paginatedResult = await _attendanceService.GetPagedAsync(filter);

            return Ok(paginatedResult);
        }
        
        [HttpPost]
        public async Task<ActionResult> RegisterAttendance(AttendanceAttendeeInsertDto attendanceAttendeeInsertDto)
        {
            if (attendanceAttendeeInsertDto == null)
            {
                return BadRequest();
            }
            var attendeeDto = await _attendeeService.GetAttendeeByIdNumberAsync(attendanceAttendeeInsertDto.Attendee.IDNumber);
            
            if (attendeeDto == null)
            {
                var attendeeInsertDto = _mapper.Map<AttendeeInsertDto>(attendanceAttendeeInsertDto.Attendee);
                //crear el attendee
                attendeeDto = await _attendeeService.CreateAsync(attendeeInsertDto);
            }
            
            //validar si ya esta registrado
            
            var attendance = await _attendanceService.GetByAttendeeIdAndExposureIdAsync(attendeeDto.AttendeeId, attendanceAttendeeInsertDto.ExposureId);
            
            if (attendance != null)
            {
                return BadRequest(new { mensaje = "Asistencia ya se encuentra registrada." });
            }
            

            var attendanceInsertDto = new AttendanceInsertDto
            {
                AttendeeId = attendeeDto.AttendeeId,
                ExposureId = attendanceAttendeeInsertDto.ExposureId
            };
            
            await _attendanceService.CreateAsync(attendanceInsertDto);
            
            return Ok(new { mensaje = "Asistencia registrada correctamente." });
        }
        
        [HttpGet("reportsxls")]
        [Authorize]
        public async Task<IActionResult> DownloadReport([FromQuery] AttendanceFilter filter)
        {
            var exposures = await _attendanceService.GetReportExcelAsync(filter);
            
            if (exposures == null)
            {
                return NotFound();
            }
            
            //return excel
            return File(exposures, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "exposures.xlsx");
        }
        
    }
}
