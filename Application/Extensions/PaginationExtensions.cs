using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SceletonAPI.Application.Models.Query;

namespace SceletonAPI.Application.Extensions
{
    public static class PaginationExtensions
    {
        public static async System.Threading.Tasks.Task<PagedResult<U>> GetPagedAsync<T, U>(this IQueryable<T> query,
                                            int page, int pageSize, IMapper mapper) where U : class
        {
            var result = new PagedResult<U>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Data = await query.Skip(skip)
                                  .Take(pageSize)
                                  .ProjectTo<U>(mapper.ConfigurationProvider)
                                  .ToListAsync();

            return result;
        }
    }
}
