using PetHome.Disscusions.Application.DisscusionManagement.Queries.GetDisscusionsWithPagination;

namespace PetHome.Disscusions.Application.Contracts;
public record GetDisscusionsWithPaginationRequest(
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetDisscusionsWithPaginationQuery ToQuery()
        => new GetDisscusionsWithPaginationQuery(SortBy, SortDirection, Page, PageSize);
}
