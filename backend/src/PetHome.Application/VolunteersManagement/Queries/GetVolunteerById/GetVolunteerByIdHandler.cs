using Microsoft.EntityFrameworkCore;
using PetHome.Application.Abstractions;
using PetHome.Application.Database;
using PetHome.Application.Dtos;
using PetHome.Domain.Shared;

namespace PetHome.Application.VolunteersManagement.Queries.GetVolunteerById
{
    public class GetVolunteerByIdHandler
        : IQueryHandler<Result<VolunteerDto>, GetVolunteerByIdQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetVolunteerByIdHandler(IReadDbContext readDbContext)
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
