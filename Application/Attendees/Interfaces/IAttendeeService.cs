using Application.Attendees.DTOs;
using Application.Common;

namespace Application.Attendees.Interfaces;

public interface IAttendeeService : ICommonService<AttendeeDto, AttendeeInsertDto, AttendeeUpdateDto>
{
    Task<AttendeeDto> GetAttendeeByIdNumberAsync(string idNumber);
}