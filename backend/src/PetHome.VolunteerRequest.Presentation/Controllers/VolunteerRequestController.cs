using Microsoft.AspNetCore.Mvc;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Framework.Controllers;
using PetHome.Shared.SharedKernel.Authorization;
using PetHome.VolunteerRequests.Application.Contracts;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.InitialRequest;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetVolunteerRequestForReview;

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
    [HttpGet]
    public async Task<ActionResult<Guid>> GetVolunteerRequestForReview(
            [FromServices] GetVolunteerRequestForReviewHandler handler,
            [FromBody] GetVolunteerRequestForReview_Request request,
            CancellationToken token)
    {
        var command = request.ToCommand();
        var result = await handler.Execute(command, token);
        return Ok(result);
    }
}
