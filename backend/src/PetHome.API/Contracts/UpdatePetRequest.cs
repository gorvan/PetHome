using PetHome.API.Extensions;
using PetHome.Application.Dtos;
using PetHome.Application.VolunteersManagement.Commands.PetManagement.UpdatePet;
using PetHome.Domain.Shared;
using System.ComponentModel;
using System.Reflection;

namespace PetHome.API.Contracts
{
    public record UpdatePetRequest(
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
        string HelpStatus,
        double Weight,
        double Height)
    {
        public UpdatePetCommand ToCommand(
            Guid VolunteerId,
            Guid PetId)
        {
            var helpStatus = HelpStatus;


            return new UpdatePetCommand(
                VolunteerId,
                PetId,
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
                HelpStatus.GetHelpStatusByDescription(),
                Weight,
                Height);
        }

        public string GetDescription(HelpStatus helpStatus)
        {
            var enum1 = helpStatus.GetType().GetMember(helpStatus.ToString()).FirstOrDefault();

            var descAttribute =
                enum1 == null
                ? default
                : enum1.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;

            var result = descAttribute == null
                ? helpStatus.ToString()
                : descAttribute.Description;

            return result;
        }
    }
}
