using Application.Attendances.DTOs;
using Application.Common;
using Domain.Filter;

namespace Application.Attendances.Interfaces;


public interface IAttendanceService : ICommonService<AttendanceDto, AttendanceInsertDto, AttendanceUpdateDto, AttendanceFilter>
{
    Task<AttendanceDto> GetByAttendeeIdAndExposureIdAsync(int attendeeId, int exposureId);
    Task<Stream> GetReportExcelAsync(AttendanceFilter filter);
}