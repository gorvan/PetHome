using PetHome.Application.Volunteers.Shared;
using PetHome.Application.VolunteersManagement.PetManagement.AddPet;
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
            double Height)
    {
        public AddPetCommand ToCommand(Guid id) =>
            new AddPetCommand(
                id,
                Nickname,
                Description,
                Color,
                Health,
                Address,
                Phone,
                Requisites,
                BirthDay,
                IsNeutered,
                IsVaccinated,
                HelpStatus,
                Weight,
                Height);
    }
}
