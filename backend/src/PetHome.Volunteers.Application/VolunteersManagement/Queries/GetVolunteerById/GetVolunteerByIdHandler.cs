using Microsoft.EntityFrameworkCore;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;

namespace PetHome.Volunteers.Application.VolunteersManagement.Queries.GetVolunteerById
{
    public class GetVolunteerByIdHandler
        : IQueryHandler<Result<VolunteerDto>, GetVolunteerByIdQuery>
    {
        private readonly IReadDbContextVolunteers _readDbContext;

        public GetVolunteerByIdHandler(IReadDbContextVolunteers readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<VolunteerDto>> Execute(
            GetVolunteerByIdQuery query,
            CancellationToken token)
        {
            var volunteerResult = await _readDbContext.Volunteers
                .FirstOrDefaultAsync(v => v.Id == query.Id, token);

            if (volunteerResult is null)
            {
                return Errors.General.NotFound(query.Id);
            }

            foreach (var item in volunteerResult.Pets)
            {
                item.SortPhotos();
            }

            return volunteerResult;
        }
    }
}
