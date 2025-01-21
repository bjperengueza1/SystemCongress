using Domain.Common.Pagination;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IExposureRepository : IRepository<Exposure>
{
    Task<PaginatedResult<Exposure>> GetExposuresByRoomPagedAsync(int roomId, int pageNumber, int pageSize);
    Task<PaginatedResult<Exposure>> GetExposuresByCongressPagedAsync(int congressId, int pageNumber, int pageSize);
    Task<Exposure> GetByGuidAsync(string guid);
}