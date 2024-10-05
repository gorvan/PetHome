using PetHome.Application.Abstractions;
using PetHome.Application.Dtos;
using PetHome.Domain.Shared;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.UpdatePet
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
