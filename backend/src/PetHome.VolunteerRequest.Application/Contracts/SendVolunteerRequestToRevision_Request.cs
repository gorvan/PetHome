using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.SendVolunteerRequestToRevision;

namespace PetHome.VolunteerRequests.Application.Contracts;
public record SendVolunteerRequestToRevision_Request(Guid DisscusionId, string Comment)
{
    public SendVolunteerRequestToRevisionCommand ToCommand(Guid VolunteerRequestId, Guid AdminId)
    {
        return new SendVolunteerRequestToRevisionCommand(
            VolunteerRequestId,
            AdminId,
            DisscusionId,
            Comment);
    }
}

