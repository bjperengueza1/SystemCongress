using Domain.Entities;
using Domain.Filter;

namespace Domain.Interfaces;

public interface IAttendeeRepository : IRepository<Attendee, AttendeeFilter>
{
    Task<Attendee> GetAttendeeByIdNumberAsync(string idNumber);
    Task<string> GetGuidCertificateAttendanceAsync(int congressId, int attendeeId);
}