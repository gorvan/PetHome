using PetHome.Application.VolunteersManagement.Queries.GetVolunteersWithPagination;

namespace PetHome.API.Contracts
{
    public record GetVolunteersWithPaginationFilteredRequest(int? Experience, int Page, int PageSize)
    {
        public GetVolunteersWithPaginationFilteredQuery ToQuery() =>
            new(Experience, Page, PageSize);
    }
}
