namespace OneLayer_ASP.NET_API_Template;
public static class PagedResponse
{
    public static async Task<PagedResult<Entity>> GetPagedResultAsync<Entity>(IQueryable<Entity> query, int pageSize, int pageNumber)
    {
        var totalItems = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Entity>(pageSize, pageNumber, totalItems, items);
    }
}

