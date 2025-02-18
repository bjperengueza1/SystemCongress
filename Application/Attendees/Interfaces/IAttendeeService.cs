using Application.Attendees.DTOs;
using Application.Common;
using Domain.Filter;

namespace Application.Attendees.Interfaces;

public interface IAttendeeService : ICommonService<AttendeeDto, AttendeeInsertDto, AttendeeUpdateDto, AttendeeFilter>
{
    Task<AttendeeDto> GetAttendeeByIdNumberAsync(string idNumber);
}