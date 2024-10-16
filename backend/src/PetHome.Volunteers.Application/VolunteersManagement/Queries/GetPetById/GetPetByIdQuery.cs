using PetHome.Shared.Core.Abstractions;

namespace PetHome.Volunteers.Application.VolunteersManagement.Queries.GetPetById
{
    public record GetPetByIdQuery(Guid petId) : IQuery;
}
