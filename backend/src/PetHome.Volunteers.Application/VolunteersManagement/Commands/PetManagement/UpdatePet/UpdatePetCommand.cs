using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.UpdatePet
{
    public record UpdatePetCommand(
        Guid VolunteerId,
        Guid PetId,
        string Nickname,
        Guid SpeciesId,
        Guid BreedId,
        string Description,
        string Color,
        string Health,
        AddressDto Address,
        string Phone,
        RequisiteDto Requisites,
        DateTime BirthDay,
        bool IsNeutered,
        bool IsVaccinated,
        HelpStatus HelpStatus,
        double Weight,
        double Height) : ICommand;
}
