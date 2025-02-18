using Domain.Dtos;
using Domain.Entities;
using Domain.Filter;

namespace Domain.Interfaces;

public interface ICongressRepository : IRepository<Congress, CongressFilter>
{
    Task<Congress> GetByGuidAsync(string guid);
    Task<IEnumerable<CongressCertificate>> GetCertificatesByDniAsync(string dni);
    Task<Congress> GetActiveAsync();
}