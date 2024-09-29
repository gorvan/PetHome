using PetHome.Application.Abstractions;

namespace PetHome.Application.VolunteersManagement.Queries.GetVolunteerById
{
    public record GetVolunteerByIdQuery(Guid Id) : IQuery;
    
}
