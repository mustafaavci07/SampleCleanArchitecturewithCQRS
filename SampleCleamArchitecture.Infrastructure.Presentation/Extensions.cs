

namespace SampleCleanArchitecture.Infrastructure.Persistence
{

    public static class GetPagedResultExtension
    {
        public async static Task<PagedList<T>> GetPagedResultListAsync<T>(this IQueryable<T> query, int pageOffset, int pageSize)
        {
            PagedList<T> result = new();
            result.Total = await query.CountAsync();
            result.List=await query.Skip(pageOffset).Take(pageSize).ToListAsync();
            result.CurrentOffset = pageOffset;
            result.PageSize = pageSize;

            return result;
        }
    }

}
