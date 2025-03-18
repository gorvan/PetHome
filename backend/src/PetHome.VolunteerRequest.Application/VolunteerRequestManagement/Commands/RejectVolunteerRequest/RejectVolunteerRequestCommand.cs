using PetHome.Shared.Core.Abstractions;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.RejectVolunteerRequest;
public record RejectVolunteerRequestCommand(
    Guid VolunteerRequestId):ICommand;
