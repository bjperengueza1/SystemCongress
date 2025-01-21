using Application.Attendances.DTOs;
using Application.Common;

namespace Application.Attendances.Interfaces;


public interface IAttendanceService : ICommonService<AttendanceDto, AttendanceInsertDto, AttendanceUpdateDto>
{
    Task<AttendanceDto> GetByAttendeeIdAndExposureIdAsync(int attendeeId, int exposureId);
}