using PetHome.Shared.Core.Abstractions;

namespace PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.DeleteMessage;
public record DeleteMessageCommand(Guid DisscusionId, Guid MessageId, Guid UserId) : ICommand;
