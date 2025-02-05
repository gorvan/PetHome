using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Models;
using PetHome.Shared.Core.Shared;
using System.Linq.Expressions;

namespace PetHome.Disscusions.Application.DisscusionManagement.Queries.GetDisscusionsWithPagination;
public class GetDisscusionsWithPaginationHandler : 
    IQueryHandler<Result<PagedList<DisscusionDto>>, GetDisscusionsWithPaginationQuery>
{
    private readonly IReadDbContextDisscusions _readDbContext;

    public GetDisscusionsWithPaginationHandler(IReadDbContextDisscusions readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<PagedList<DisscusionDto>>> Execute(
        GetDisscusionsWithPaginationQuery query,
        CancellationToken token)
    {
        var disscusionQuery = _readDbContext.Disscusions;

        Expression<Func<DisscusionDto, object>> keySelector
            = query.SortBy?.ToLower() switch
            {                           
                _ => (v) => v.State,
            };

        disscusionQuery = query.SortDirection?.ToLower() == Constants.SORT_DESCENDING
            ? disscusionQuery.OrderByDescending(d=>d.State)
            : disscusionQuery.OrderBy(d => d.State);
                
        return await disscusionQuery.ToPagedList(query.Page, query.PageSize, token);
    }
}
