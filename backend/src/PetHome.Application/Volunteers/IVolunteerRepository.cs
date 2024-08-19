using PetHome.Domain.Shared;
using PetHome.Domain.Models.Volunteers;

namespace PetHome.Application.Volunteers
{
    public interface IVolunteerRepository
    {
        Task<Result<Guid>> Add(Volunteer volunteer, CancellationToken token);
        Task<Result<Volunteer>> GetById(VolunteerId id, CancellationToken token);
        Task<Result<Volunteer>> GetByName(FullName fullName, CancellationToken token);
    }
}
