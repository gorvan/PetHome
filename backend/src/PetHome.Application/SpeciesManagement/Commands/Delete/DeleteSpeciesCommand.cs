using PetHome.Application.Abstractions;

namespace PetHome.Application.SpeciesManagement.Commands.Delete
{
    public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;
    
}
