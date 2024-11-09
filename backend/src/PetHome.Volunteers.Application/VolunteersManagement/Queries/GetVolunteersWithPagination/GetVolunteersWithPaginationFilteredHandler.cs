using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Models;
using PetHome.Shared.Core.Shared;
using System.Linq.Expressions;

namespace PetHome.Volunteers.Application.VolunteersManagement.Queries.GetVolunteersWithPagination
{
    public class GetVolunteersWithPaginationFilteredHandler
        : IQueryHandler<Result<PagedList<VolunteerDto>>, GetVolunteersWithPaginationFilteredQuery>
    {
        private readonly IReadDbContextVolunteers _readDbContext;

        public GetVolunteersWithPaginationFilteredHandler(
            IReadDbContextVolunteers readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<PagedList<VolunteerDto>>> Execute(
            GetVolunteersWithPaginationFilteredQuery query,
            CancellationToken token)
        {
            var volunteerQuery = _readDbContext.Volunteers;

            Expression<Func<VolunteerDto, object>> keySelector
                = query.SortBy?.ToLower() switch
                {
                    "firstname" => (v) => v.FirstName,
                    "secondname" => (v) => v.SecondName,
                    "surname" => (v) => v.Surname,
                    "email" => (v) => v.Email,
                    "phone" => (v) => v.Phone,
                    "description" => (v) => v.Description,
                    "experience" => (v) => v.Experience,
                    _ => (v) => v.FirstName,
                };

            volunteerQuery = query.SortDirection?.ToLower() == Constants.SORT_DESCENDING
                ? volunteerQuery.OrderByDescending(keySelector)
                : volunteerQuery.OrderBy(keySelector);

            volunteerQuery = volunteerQuery.WhereIf(
                query.Experience > 0,
                v => v.Experience == query.Experience);

            return await volunteerQuery
                    .ToPagedList(query.Page, query.PageSize, token);
        }
    }
}
