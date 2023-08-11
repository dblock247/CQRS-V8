using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Helpers;

public class PagedResult<T> where T : class
{
    public static async Task<PagedResult<T>> GetPagedResult(IQueryable<T> data, int pageSize, int page, CancellationToken cancellationToken)
    {
        var result = new PagedResult<T>()
        {
            TotalRows = data.Count(),
            Page = page,
            PageSize = pageSize,
            Data = await data.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken)
        };

        return result;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalRows { get; set; }
    public IList<T> Data { get; set; }
}
