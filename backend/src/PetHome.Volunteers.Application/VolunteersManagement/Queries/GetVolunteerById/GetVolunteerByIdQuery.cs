using PetHome.Shared.Core.Abstractions;

namespace PetHome.Volunteers.Application.VolunteersManagement.Queries.GetVolunteerById
{
    public record GetVolunteerByIdQuery(Guid Id) : IQuery;

}
