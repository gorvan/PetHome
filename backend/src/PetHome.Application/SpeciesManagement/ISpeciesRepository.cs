using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using PetHome.Domain.SpeciesManagement.Entities;

namespace PetHome.Application.SpeciesManagement
{
    public interface ISpeciesRepository
    {
        Task<Result<Guid>> Add(Species species, CancellationToken token);
        Task<Result<Guid>> Update(Species species, CancellationToken token);
        Task<Result<Guid>> Delete(Species species, CancellationToken token);
        Task<Result<Species>> GetById(SpeciesId speciesId, CancellationToken token);
    }
}
