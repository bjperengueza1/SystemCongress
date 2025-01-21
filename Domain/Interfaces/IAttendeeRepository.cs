using Domain.Entities;

namespace Domain.Interfaces;

public interface IAttendeeRepository : IRepository<Attendee>
{
    Task<Attendee> GetAttendeeByIdNumberAsync(string idNumber);
}