using PetHome.Shared.Core.Dtos;

namespace PetHome.Volunteers.Application
{
    public interface IReadDbContextVolunteers
    {
        IQueryable<VolunteerDto> Volunteers { get; }
        IQueryable<PetDto> Pets { get; }
    }
}
