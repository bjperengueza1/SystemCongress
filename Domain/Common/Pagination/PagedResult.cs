namespace Domain.Common.Pagination;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    
    public PagedResult<U> Map<U>(Func<T, U> transform)
    {
        if (transform == null) throw new ArgumentNullException(nameof(transform));

        var transformedItems = Items.Select(transform).ToList();
        return new PagedResult<U>
        {
            Items = transformedItems,
            TotalItems = TotalItems,
            PageNumber = PageNumber,
            PageSize = PageSize
        };
        //return new PagedResult<U>(transformedItems, TotalItems, PageNumber, PageSize);
    }
}