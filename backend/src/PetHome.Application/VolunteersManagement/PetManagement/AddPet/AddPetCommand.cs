using PetHome.Application.Volunteers.Shared;
using PetHome.Domain.Shared;

namespace PetHome.Application.VolunteersManagement.PetManagement.AddPet
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
            double Height);
}
