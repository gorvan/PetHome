using Microsoft.AspNetCore.Mvc;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Framework.Controllers;
using PetHome.Shared.SharedKernel.Authorization;
using PetHome.VolunteerRequests.Application.Contracts;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.AproveVolunteerRequest;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.CreateInitialVolunteerRequest;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.GetVolunteerRequestForReview;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.RejectVolunteerRequest;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.SendVolunteerRequestToRevision;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.UpdateVolunteerRequest;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetNotReviewedVolunteerRequests;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetReviewedVolunteerRequestsByAdmin;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetVounteerRequestsByVolunteer;

namespace PetHome.VolunteerRequests.Presentation.Controllers;
public class VolunteerRequestController : ApplicationContoller
{
    [Permission(Permissions.Participant.ReadParticipant)]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateInitialRequest(
            [FromServices] InitialRequestHandler handler,
            [FromBody] CreateInitialRequest_Request request,
            CancellationToken token)
    {
        var command = request.ToCommand();
        var result = await handler.Execute(command, token);
        return result.ToResponse();
    }

    [Permission(Permissions.Admin.CreateVolunteer)]
    [HttpPut("{adminId:guid}/get_request_on_review/{requestId:guid}")]
    public async Task<ActionResult<Guid>> GetVolunteerRequestForReview(
            [FromServices] GetVolunteerRequestForReviewHandler handler,
            [FromBody] GetVolunteerRequestForReview_Request request,
            [FromRoute] Guid adminId,
            [FromRoute] Guid requestId,
            CancellationToken token)
    {
        var command = request.ToCommand(requestId, adminId);
        var result = await handler.Execute(command, token);
        return Ok(result);
    }

    [Permission(Permissions.Admin.CreateVolunteer)]
    [HttpPut("{adminId:guid}/send_to_revision/{requestId:guid}")]
    public async Task<ActionResult<Guid>> SendVolunteerRequestToRevision(
           [FromServices] SendVolunteerRequestToRevisionHandler handler,
           [FromBody] SendVolunteerRequestToRevision_Request request,
           [FromRoute] Guid adminId,
           [FromRoute] Guid requestId,
           CancellationToken token)
    {
        var command = request.ToCommand(requestId, adminId);
        var result = await handler.Execute(command, token);
        return Ok(result);
    }

    
    [Permission(Permissions.Admin.CreateVolunteer)]
    [HttpPut("/reject/{requestId:guid}")]
    public async Task<ActionResult<Guid>> RejectVolunteerRequest(
           [FromServices] RejectVolunteerRequestHandler handler,
           [FromRoute] Guid requestId,
           CancellationToken token)
    {
        var command = new RejectVolunteerRequestCommand(requestId);
        var result = await handler.Execute(command, token);
        return Ok(result);
    }

    [Permission(Permissions.Admin.CreateVolunteer)]
    [HttpPut("/aprove/{requestId:guid}")]
    public async Task<ActionResult<Guid>> AproveVolunteerRequest(
          [FromServices] AproveVolunteerRequestHandler handler,
          [FromRoute] Guid requestId,
          CancellationToken token)
    {
        var command = new AproveVolunteerRequestCommand(requestId);
        var result = await handler.Execute(command, token);
        return Ok(result);
    }

    [Permission(Permissions.Participant.ReadParticipant)]
    [HttpPut("/update/{requestId:guid}")]
    public async Task<ActionResult<Guid>> UpdateRequest(
            [FromServices] UpdateVolunteerRequestHandler handler,
            [FromBody] UpdateVolunteerRequest_Request request,
            [FromRoute] Guid requestId,
            CancellationToken token)
    {
        var command = request.ToCommand(requestId);
        var result = await handler.Execute(command, token);
        return result.ToResponse();
    }

    [Permission(Permissions.Admin.CreateVolunteer)]
    [HttpGet("{adminId:guid}/not_reviewed")]
    public async Task<ActionResult<DisscusionDto>> GetNotReviewedVolunteerRequests(
           [FromServices] GetNotReviewedVolunteerRequestsHandler handler,
           [FromBody] GetNotReviewedVolunteerRequests_Request request,
           [FromRoute] Guid adminId,
           CancellationToken token)
    {
        var query = request.ToQuery(adminId);
        var responce = await handler.Execute(query, token);
        return Ok(responce);
    }

    [Permission(Permissions.Admin.CreateVolunteer)]
    [HttpGet("{adminId:guid}/on_reviwe")]
    public async Task<ActionResult<DisscusionDto>> GetReviewedVolunteerRequestsByAdmin(
           [FromServices] GetReviewedVolunteerRequestsByAdminHandler handler,
           [FromBody] GetReviewedVolunteerRequestsByAdminRequest request,
           [FromRoute] Guid adminId,
           CancellationToken token)
    {
        var query = request.ToQuery(adminId);
        var responce = await handler.Execute(query, token);
        return Ok(responce);
    }

    [Permission(Permissions.Admin.CreateVolunteer)]
    [HttpGet("{volunteerId:guid}/volunteer_requests")]
    public async Task<ActionResult<DisscusionDto>> GetVolunteerRequestsByVolunteer(
           [FromServices] GetVounteerRequestsByVolunteerHandler handler,
           [FromBody] GetVounteerRequestsByVolunteerRequest request,
           [FromRoute] Guid volunteerId,
           CancellationToken token)
    {
        var query = request.ToQuery(volunteerId);
        var responce = await handler.Execute(query, token);
        return Ok(responce);
    }
}
