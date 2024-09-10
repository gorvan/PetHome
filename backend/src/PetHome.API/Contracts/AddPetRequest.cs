using PetHome.Application.Volunteers.Shared;
using PetHome.Domain.Shared;

namespace PetHome.Application.Volunteers.AddPet
{
    public record AddPetRequest
            (string Nickname,
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
