using Domain.Entities;
using Domain.Filter;

namespace Domain.Interfaces;

public interface IAttendanceRepository : IRepository<Attendance, AttendanceFilter>
{
    Task<Attendance> GetByAttendeeIdAndExposureIdAsync(int attendeeId, int exposureId);
}