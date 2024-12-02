namespace Domain.Interfaces;

public interface IRepository<T> where T : class
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
    
    // Save changes async
    Task SaveAsync();
}