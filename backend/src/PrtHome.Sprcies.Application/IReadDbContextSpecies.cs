using PetHome.Shared.Core.Dtos;

namespace PetHome.Species.Application
{
    public interface IReadDbContextSpecies
    {
        IQueryable<SpeciesDto> Species { get; }
        IQueryable<BreedDto> Breeds { get; }
    }
}
