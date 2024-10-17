using Microsoft.EntityFrameworkCore;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;

namespace PetHome.Volunteers.Application.VolunteersManagement.Queries.GetPetById
{
    public class GetPetByIdHandler
        : IQueryHandler<Result<PetDto>, GetPetByIdQuery>
    {
        private readonly IReadDbContextVolunteers _readDbContext;

        public GetPetByIdHandler(IReadDbContextVolunteers readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<PetDto>> Execute(
            GetPetByIdQuery query,
            CancellationToken token)
        {
            var petResult = await _readDbContext.Pets
                .FirstOrDefaultAsync(v => v.Id == query.petId, token);

            if (petResult is null)
            {
                return Errors.General.NotFound(query.petId);
            }

            petResult.SortPhotos();

            return petResult;
        }
    }
}
