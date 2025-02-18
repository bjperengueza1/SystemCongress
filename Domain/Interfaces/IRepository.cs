using Domain.Common.Pagination;
using Domain.Filter;

namespace Domain.Interfaces;

public interface IRepository<T,TF> where T : class where TF : Filter.Filter
{
    // Get all entities async
    Task<IEnumerable<T>> GetAllAsync();
    
    // Get entity by id async
    Task<T> GetByIdAsync(int id);
    
    // Add entity async
    Task AddAsync(T entity);
    
    // Update entity
    void UpdateAsync(T entity);
    
    // Delete entity
    void DeleteAsync(T entity);
    
    //change state of entity
    //void ChangeState(T entity, EntityState state);
    
    //disable entity
    //void Disable(T entity);
    
    //enable entity
    //void Enable(T entity);
    
    // Save changes async
    Task SaveAsync();
    
    // Get paged entities async
    Task<PaginatedResult<T>> GetPagedAsync(TF tf);
}