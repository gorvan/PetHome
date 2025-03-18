using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.UpdateVolunteerRequest;
public record UpdateVolunteerRequestCommand(
    Guid VolunteerRequestId,
    Guid UserId,
    FullNameDto FullName,
    string Email,
    string Description,
    string Phone,
    RequestStatus Status,
    DateTime CreateAt) : ICommand;
