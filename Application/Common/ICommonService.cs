using Domain.Common.Pagination;

namespace Application.Common;

public interface ICommonService<T,TI,TU, TF>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> CreateAsync(TI ti);
    Task<T> UpdateAsync(int id, TU tu);
    Task<PaginatedResult<T>> GetPagedAsync(TF tf);
}