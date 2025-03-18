using PetHome.Shared.Core.Abstractions;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.SendVolunteerRequestToRevision;
public record SendVolunteerRequestToRevisionCommand(
    Guid VolunteerRequestId,
    Guid AdminId,
    Guid DisscusionId,
    string Comment) : ICommand;

