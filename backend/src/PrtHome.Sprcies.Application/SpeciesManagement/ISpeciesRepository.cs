using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Species.Domain;

namespace PetHome.Species.Application.SpeciesManagement
{
    public interface ISpeciesRepository
    {
        Task<Result<Guid>> Add(SpeciesType species, CancellationToken token);
        Task<Result<Guid>> Update(SpeciesType species, CancellationToken token);
        Task<Result<Guid>> Delete(SpeciesType species, CancellationToken token);
        Task<Result<SpeciesType>> GetById(SpeciesId speciesId, CancellationToken token);
    }
}
