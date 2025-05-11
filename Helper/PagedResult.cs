namespace OneLayer_ASP_NET_API_Template.Helper;

public class PagedResult<T>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }

    public List<T> Items { get; set; }
    public bool HasNextPage => PageSize * PageNumber < TotalItems;
    public bool HasPreveiosPage => PageNumber > 1;


    private PagedResult()
    {

    }

    public PagedResult(int pageSize, int pageNumber, int totalItems, List<T> items)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalItems = totalItems;
        Items = items;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }
}