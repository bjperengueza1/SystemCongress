using Domain.Entities;
using Domain.Filter;

namespace Domain.Interfaces;

public interface IAuthorRepository : IRepository<Author, AuthorFilter>
{
    Task<Author?> GetByIdNumberAsync(string idNumber);
    
    Task<IEnumerable<Author>> GetAuthorsByExposureAsync(int exposureId);
    
    Task<Author> GetFirstAuthorByExposureAsync(int exposureId);
}