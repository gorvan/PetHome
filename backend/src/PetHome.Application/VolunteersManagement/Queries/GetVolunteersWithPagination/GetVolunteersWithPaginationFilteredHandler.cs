using PetHome.Application.Abstractions;
using PetHome.Application.Database;
using PetHome.Application.Dtos;
using PetHome.Application.Extensions;
using PetHome.Application.Models;
using PetHome.Domain.Shared;

namespace PetHome.Application.VolunteersManagement.Queries.GetVolunteersWithPagination
{
    public class GetVolunteersWithPaginationFilteredHandler
        : IQueryHandler<Result<PagedList<VolunteerDto>>, GetVolunteersWithPaginationFilteredQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetVolunteersWithPaginationFilteredHandler(
            IReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<PagedList<VolunteerDto>>> Execute(
            GetVolunteersWithPaginationFilteredQuery query,
            CancellationToken token)
        {
            var volunteerQuery = _readDbContext.Volunteers;

            if (query.Experience > 0)
            {
                volunteerQuery = volunteerQuery
                    .Where(v => v.Experience == query.Experience);
            }

            return await volunteerQuery.ToPagedList(query.Page, query.PageSize, token);
        }
    }
}
