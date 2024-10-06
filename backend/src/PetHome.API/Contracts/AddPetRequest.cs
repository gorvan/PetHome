using PetHome.Application.Dtos;
using PetHome.Application.VolunteersManagement.Commands.PetManagement.AddPet;
using PetHome.Domain.Shared;

namespace PetHome.Application.Volunteers.AddPet
{
    public record AddPetRequest
            (string Nickname,
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
            string HelpStatus,
            double Weight,
            double Height)
    {
        public AddPetCommand ToCommand(Guid id) =>
            new AddPetCommand(
                id,
                Nickname,
                SpeciesId,
                BreedId,
                Description,
                Color,
                Health,
                Address,
                Phone,
                Requisites,
                BirthDay,
                IsNeutered,
                IsVaccinated,
                (HelpStatus)Enum.Parse(typeof(HelpStatus), HelpStatus),
                Weight,
                Height);
    }
}
