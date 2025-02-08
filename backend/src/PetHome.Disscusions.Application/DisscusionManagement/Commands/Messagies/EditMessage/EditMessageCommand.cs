using PetHome.Shared.Core.Abstractions;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.EditMessage;
public record EditMessageCommand(
    Guid DisscusionId, 
    Guid MessageId, 
    string NewMessage,
    Guid UserId) : ICommand;
