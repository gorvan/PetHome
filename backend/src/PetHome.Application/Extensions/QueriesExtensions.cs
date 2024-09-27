using Microsoft.EntityFrameworkCore;
using PetHome.Application.Models;

namespace PetHome.Application.Extensions
{
    public static class QueriesExtensions
    {
        public static async Task<PagedList<T>> ToPagedList<T>(
            this IQueryable<T> source,
            int page,
            int pageSize,
            CancellationToken token)
        {
            var totalCount = await source.CountAsync(token);

            var items = await source
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);

            return new PagedList<T>
            {
                Items = items,
                PageSize = pageSize,
                Page = page,
                TotalCount = totalCount
            };
        }
    }
}
