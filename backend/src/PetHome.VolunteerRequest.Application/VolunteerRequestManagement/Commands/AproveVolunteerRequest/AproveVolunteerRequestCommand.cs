using PetHome.Shared.Core.Abstractions;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.AproveVolunteerRequest;
public record AproveVolunteerRequestCommand(Guid VolunteerRequestId) : ICommand; 
