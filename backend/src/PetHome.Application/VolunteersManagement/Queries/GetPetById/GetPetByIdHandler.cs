using Microsoft.EntityFrameworkCore;
using PetHome.Application.Abstractions;
using PetHome.Application.Database;
using PetHome.Application.Dtos;
using PetHome.Domain.Shared;

namespace PetHome.Application.VolunteersManagement.Queries.GetPetById
{
    public class GetPetByIdHandler
        : IQueryHandler<Result<PetDto>, GetPetByIdQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetPetByIdHandler(IReadDbContext readDbContext)
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
