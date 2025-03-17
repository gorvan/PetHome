using PetHome.Shared.Core.Dtos;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement;
public interface IreadDbContextVolunteerRequest
{
    IQueryable<VolunteerRequestDto> VolunteerRequests {  get; }
}
