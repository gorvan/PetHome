using PetHome.Application.Abstractions;

namespace PetHome.Application.VolunteersManagement.Queries.GetPetById
{
    public record GetPetByIdQuery(Guid petId) : IQuery;
}
