using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.UpdateVolunteerRequest;

namespace PetHome.VolunteerRequests.Application.Contracts;
public record UpdateVolunteerRequest_Request
       (Guid UserId,
        FullNameDto FullNameDto,
        string Email,
        string Description,
        string Phone,
        DateTime CreateAt)
{
    public UpdateVolunteerRequestCommand ToCommand(Guid requestId) =>
        new UpdateVolunteerRequestCommand(
             requestId,
             UserId,
             FullNameDto,
             Email,
             Description,
             Phone,
             RequestStatus.None,
             CreateAt);
}
