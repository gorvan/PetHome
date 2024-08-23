using PetHome.Domain.Shared;
using PetHome.Domain.PetManadgement.AggregateRoot;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Application.Volunteers
{
    public interface IVolunteerRepository
    {
        Task<Result<Guid>> Add(Volunteer volunteer, CancellationToken token);
        Task<Result<Volunteer>> GetById(VolunteerId id, CancellationToken token);
        Task<Result<Volunteer>> GetByPhone(Phone phone, CancellationToken token);
    }
}
