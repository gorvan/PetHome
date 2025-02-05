using Microsoft.AspNetCore.Mvc;
using PetHome.Disscusions.Application.Contracts;
using PetHome.Disscusions.Application.DisscusionManagement.Commands.CloseDisscusion;
using PetHome.Disscusions.Application.DisscusionManagement.Commands.CreateDisscusion;
using PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.AddMessage;
using PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.DeleteMessage;
using PetHome.Disscusions.Application.DisscusionManagement.Commands.Messagies.EditMessage;
using PetHome.Disscusions.Application.DisscusionManagement.Queries.GetDisscusionByRealtionId;
using PetHome.Disscusions.Application.DisscusionManagement.Queries.GetDisscusionsWithPagination;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Framework.Controllers;
using PetHome.Shared.SharedKernel.Authorization;

namespace PetHome.Disscusions.Presentation.Controllers;
public class DisscusionConrtoller : ApplicationContoller
{
    [Permission(Permissions.Admin.CreateVolunteer)]
    [HttpGet]
    public async Task<ActionResult> GetDisscusions(
           [FromServices] GetDisscusionsWithPaginationHandler handler,
           [FromBody] GetDisscusionsWithPaginationRequest request,
           CancellationToken token)
    {
        var query = request.ToQuery();
        var responce = await handler.Execute(query, token);
        return Ok(responce);
    }

    [Permission(Permissions.Admin.CreateVolunteer)]
    [HttpGet("{relationId:guid}")]
    public async Task<ActionResult<DisscusionDto>> GetDisscusionByRealtionId(
           [FromServices] GetDisscusionByRelationIdHandler handler,           
           [FromRoute] Guid relationId,
           CancellationToken token)
    {
        var query = new GetDisscusionByRelationIdQuery(relationId);
        var responce = await handler.Execute(query, token);
        return Ok(responce);
    }

    [Permission(Permissions.Admin.CreateVolunteer)]
    [HttpPost("create")]
    public async Task<ActionResult<Guid>> Create(
            [FromServices] CreateDisscusionHandler handler,
            [FromBody] CreateDisscusionRequest request,
            CancellationToken token)
    {
        var command = request.ToCommand();
        var result = await handler.Execute(command, token);
        return result.ToResponse();
    }

    [Permission(Permissions.Admin.CreateVolunteer)]
    [HttpDelete("{disscussionId:guid}")]
    public async Task<ActionResult<Guid>> Close(
            [FromServices] CloseDisscusionHandler handler,
            [FromRoute] Guid disscussionId,
            CancellationToken token)
    {
        var command = new CloseDisscusionCommand(disscussionId);
        var result = await handler.Execute(command, token);
        return result.ToResponse();
    }

    [Permission(Permissions.Participant.ReadParticipant)]
    [HttpPost("{disscusionId:guid}/user/{userId:guid}/message")]
    public async Task<ActionResult<Guid>> AddMessage(
            [FromServices] AddMessageHandler handler,
            [FromBody] AddMessageRequest request,
            [FromRoute] Guid disscusionId,
            [FromRoute] Guid userId,
            CancellationToken token)
    {
        var command = request.ToCommand(disscusionId, userId);
        var result = await handler.Execute(command, token);
        return result.ToResponse();
    }

    [Permission(Permissions.Participant.ReadParticipant)]
    [HttpPut("{disscusionId:guid}/user/{userId:guid}/message/{messageId:guid}/edit")]
    public async Task<ActionResult<Guid>> EditMessage(
            [FromServices] EditMessageHandler handler,
            [FromBody] EditMessageRequest request,
            [FromRoute] Guid disscusionId,
            [FromRoute] Guid userId,
            [FromRoute] Guid messageId,
            CancellationToken token)
    {
        var command = request.ToCommand(disscusionId, messageId, userId);
        var result = await handler.Execute(command, token);
        return result.ToResponse();
    }

    [Permission(Permissions.Participant.ReadParticipant)]
    [HttpDelete("{disscusionId:guid}/user/{userId:guid}/message/{messageId:guid}")]
    public async Task<ActionResult<Guid>> DeleteMessage(
            [FromServices] DeleteMessageHandler handler,
            [FromRoute] Guid disscusionId,
            [FromRoute] Guid userId,
            [FromRoute] Guid messageId,
            CancellationToken token)
    {
        var command = new DeleteMessageCommand(disscusionId, messageId, userId);
        var result = await handler.Execute(command, token);
        return result.ToResponse();
    }
}
