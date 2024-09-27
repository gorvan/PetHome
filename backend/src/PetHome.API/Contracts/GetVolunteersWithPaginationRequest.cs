using PetHome.Application.VolunteersManagement.Queries.GetVolunteersWithPagination;

namespace PetHome.API.Contracts
{
    public record GetVolunteersWithPaginationRequest(int Page, int PageSize)
    {
        public GetVolunteersWithPaginationQuery ToQuery() =>
            new(Page, PageSize);
    }
}
