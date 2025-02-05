using PetHome.Shared.Core.Abstractions;

namespace PetHome.Disscusions.Application.DisscusionManagement.Queries.GetDisscusionsWithPagination;
public record GetDisscusionsWithPaginationQuery(
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;

