using Microsoft.Extensions.Logging;
using PetHome.Application.Abstractions;
using PetHome.Application.Database;
using PetHome.Application.Dtos;
using PetHome.Application.Extensions;
using PetHome.Application.Models;

namespace PetHome.Application.VolunteersManagement.Queries.GetVolunteersWithPagination
{
    public class GetVolunteersWithPaginationHandler 
        : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
    {
        private readonly IReadDbContext _readDbContext;
        private readonly ILogger<GetVolunteersWithPaginationHandler> _logger;

        public GetVolunteersWithPaginationHandler(
            IReadDbContext readDbContext,
            ILogger<GetVolunteersWithPaginationHandler> logger)
        {
            _readDbContext = readDbContext;
            _logger = logger;
        }

        public async Task<PagedList<VolunteerDto>> Execute(
            GetVolunteersWithPaginationQuery query,
            CancellationToken token)
        {
            var volunteerQuery = _readDbContext.Volunteers;

            return await volunteerQuery.ToPagedList(query.Page, query.PageSize, token);
        }
    }
}
