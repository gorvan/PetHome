using PetHome.Shared.Core.Shared;
using PetHome.VolunteerRequests.Domain;
using PetHome.VolunteerRequests.Domain.ValueObjects;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement;
public interface IVolunteerRequestRepository
{
    Task<Result<Guid>> Add(VolunteerRequest volunteerRequest, CancellationToken token);
    Task<Result<Guid>> Update(VolunteerRequest volunteerRequest, CancellationToken token);
    Task<Result<Guid>> Delete(VolunteerRequest volunteerRequest, CancellationToken token);
    Task<Result<VolunteerRequest>> GetById(RequestId id, CancellationToken token);
    Result<IEnumerable<VolunteerRequest>> GetNotReviewedVolunteerRequests(CancellationToken token);
}
