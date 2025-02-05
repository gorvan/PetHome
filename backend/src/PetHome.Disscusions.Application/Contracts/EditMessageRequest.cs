using PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.EditMessage;

namespace PetHome.Disscusions.Application.Contracts;
public record EditMessageRequest(string NewMessage)
{
    public EditMessageCommand ToCommand(Guid disscusionId, Guid messageId, Guid userId) =>
        new EditMessageCommand(disscusionId, messageId, NewMessage, userId);
}
