using Domain.Entities;

namespace Domain.Interfaces;

public interface ICongressRepository : IRepository<Congress>
{
    Task<Congress> GetByGuidAsync(string guid);
}