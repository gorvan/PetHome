﻿using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;
using PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.UpdatePet;

namespace PetHome.Volunteers.Application.Contracts
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
                (HelpStatus)Enum.Parse(typeof(HelpStatus), HelpStatus),
                Weight,
                Height);
        }
    }
}
