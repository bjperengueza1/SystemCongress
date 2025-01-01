namespace Domain.Common.Pagination;

public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    
    public static PaginatedResult<T> Create(IEnumerable<T> items, int totalItems, int pageNumber, int pageSize)
    {
        return new PaginatedResult<T>
        {
            Items = items,
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
    
    public PaginatedResult<TU> Map<TU>(Func<T, TU> transform)
    {
        if (transform == null) throw new ArgumentNullException(nameof(transform));

        var transformedItems = Items.Select(transform).ToList();
        return new PaginatedResult<TU>
        {
            Items = transformedItems,
            TotalItems = TotalItems,
            PageNumber = PageNumber,
            PageSize = PageSize
        };
    }
}