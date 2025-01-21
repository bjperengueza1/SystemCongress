using Domain.Entities;

namespace Domain.Interfaces;

public interface IAttendanceRepository : IRepository<Attendance>
{
    Task<Attendance> GetByAttendeeIdAndExposureIdAsync(int attendeeId, int exposureId);
}