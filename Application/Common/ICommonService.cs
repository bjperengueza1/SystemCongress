
using Domain.Common.Pagination;

namespace Application.Common;

public interface ICommonService<T,TI,TU>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> CreateAsync(TI ti);
    Task<T> UpdateAsync(int id, TU tu);
    
    Task<PagedResult<T>> GetPagedAsync(int pageNumber, int pageSize);
    
}