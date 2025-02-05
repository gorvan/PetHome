
using PetHome.Shared.Core.Abstractions;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.AddMessage;
public record AddMessageCommand(
    Guid DisscusionId,
    string Message, 
    Guid UserId) : ICommand;
