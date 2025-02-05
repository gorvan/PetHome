using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.VolunteerRequests.Domain;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement;
public interface IVolunteerRequestRepository 
{
    Task<Result<Guid>> Add(VolunteerRequest volunteerRequest, CancellationToken token);
    Task<Result<Guid>> Update(VolunteerRequest volunteerRequest, CancellationToken token);
    Task<Result<Guid>> Delete(VolunteerRequest volunteerRequest, CancellationToken token);
    Task<Result<Guid>> Restore(VolunteerRequest volunteerRequest, CancellationToken token);
    Task<Result<VolunteerRequest>> GetById(VolunteerId id, CancellationToken token);
    Task<Result<VolunteerRequest>> GetByPhone(Phone phone, CancellationToken token);
}
