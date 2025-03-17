using Domain.Common.Pagination;
using Domain.Entities;
using Domain.Filter;

namespace Domain.Interfaces;

public interface IExposureRepository : IRepository<Exposure, ExposureFilter>
{
    Task<PaginatedResult<Exposure>> GetExposuresByRoomPagedAsync(int roomId, int pageNumber, int pageSize);
    Task<PaginatedResult<Exposure>> GetExposuresByCongressPagedAsync(int congressId, int pageNumber, int pageSize);
    
    Task<PaginatedResult<Exposure>> GetExposuresApprovedByCongressPagedAsync(int congressId, int pageNumber, int pageSize);
    
    Task<Exposure> GetByGuidAsync(string guid);
    
    Task<IEnumerable<Exposure>> GetAllEAsync(ExposureFilter filter);
    
    Task<bool> CheckDisponibleHoursAsync(int roomId, DateTime dateStart, DateTime dateEnd);
}