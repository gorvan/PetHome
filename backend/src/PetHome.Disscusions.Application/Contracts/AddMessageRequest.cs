using PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.AddMessage;

namespace PetHome.Disscusions.Application.Contracts;
public record AddMessageRequest(
    string Message)
{
    public AddMessageCommand ToCommand(Guid disscusionId, Guid userId) =>
        new AddMessageCommand(disscusionId, Message, userId);
}
