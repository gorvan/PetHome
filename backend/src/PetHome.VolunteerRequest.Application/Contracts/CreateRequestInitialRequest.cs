using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.CreateInitialVolunteerRequest;

namespace PetHome.VolunteerRequests.Application.Contracts;
public record CreateInitialRequest_Request(
    Guid RequestId,
    Guid UserId,
    FullNameDto FullNameDto,
    string Email,
    string Description,
    string Phone,
    DateTime CreateAt)
{
    public InitialRequestCommand ToCommand() =>
        new InitialRequestCommand(
             RequestId,
             UserId,
             FullNameDto,
             Email,
             Description,
             Phone,
             RequestStatus.None,
             CreateAt);
}
