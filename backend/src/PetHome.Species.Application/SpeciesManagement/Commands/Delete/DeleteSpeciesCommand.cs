using PetHome.Shared.Core.Abstractions;

namespace PetHome.Species.Application.SpeciesManagement.Commands.Delete
{
    public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;

}
