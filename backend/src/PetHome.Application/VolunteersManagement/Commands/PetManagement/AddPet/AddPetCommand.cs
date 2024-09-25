using PetHome.Application.Abstractions;
using PetHome.Application.Dtos;
using PetHome.Domain.Shared;

namespace PetHome.Application.VolunteersManagement.Commands.PetManagement.AddPet
{
    public record AddPetCommand
            (Guid VolunteerId,
            string Nickname,
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
