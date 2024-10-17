using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Volunteers.Domain;

namespace PetHome.Volunteers.Application.VolunteersManagement
{
    public interface IVolunteerRepository
    {
        Task<Result<Guid>> Add(Volunteer volunteer, CancellationToken token);
        Task<Result<Guid>> Update(Volunteer volunteer, CancellationToken token);
        Task<Result<Guid>> Delete(Volunteer volunteer, CancellationToken token);
        Task<Result<Guid>> Restore(Volunteer volunteer, CancellationToken token);
        Task<Result<Volunteer>> GetById(VolunteerId id, CancellationToken token);
        Task<Result<Volunteer>> GetByPhone(Phone phone, CancellationToken token);
    }
}
