using Domain.Entities;

namespace Domain.Interfaces;

public interface IAuthorRepository : IRepository<Author>
{
    Task<Author?> GetByIdNumberAsync(string idNumber);
    
    Task<IEnumerable<Author>> GetAuthorsByExposureAsync(int exposureId);
    
    Task<Author> GetFirstAuthorByExposureAsync(int exposureId);
}