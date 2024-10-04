using Microsoft.AspNetCore.Mvc;
using PetHome.API.Contracts;
using PetHome.API.Extensions;
using PetHome.API.Processors;
using PetHome.Application.Volunteers.AddPet;
using PetHome.Application.Volunteers.UpdateMainInfo;
using PetHome.Application.VolunteersManagement.Commands.Create;
using PetHome.Application.VolunteersManagement.Commands.Delete;
using PetHome.Application.VolunteersManagement.Commands.PetManagement.AddPet;
using PetHome.Application.VolunteersManagement.Commands.PetManagement.AddPetFiles;
using PetHome.Application.VolunteersManagement.Commands.Restore;
using PetHome.Application.VolunteersManagement.Commands.UpdateMainInfo;
using PetHome.Application.VolunteersManagement.Commands.UpdateRequisites;
using PetHome.Application.VolunteersManagement.Commands.UpdateSocialNetworks;
using PetHome.Application.VolunteersManagement.Queries.GetVolunteerById;
using PetHome.Application.VolunteersManagement.Queries.GetVolunteersWithPagination;
using PetHome.Application.VolunteersManagement.Queries.GetVolunteersWithPaginationDapper;

namespace PetHome.API.Controllers
{

    public class VolunteersController : ApplicationContoller
    {
        [HttpGet]
        public async Task<ActionResult> Get(
            [FromQuery] GetVolunteersWithPaginationFilteredRequest request,
            [FromServices] GetVolunteersWithPaginationFilteredHandler handler,
            CancellationToken token)
        {
            var query = request.ToQuery();
            var response = await handler.Execute(query, token);
            return Ok(response);
        }

        [HttpGet("dapper")]
        public async Task<ActionResult> Get(
            [FromQuery] GetVolunteersWithPaginationFilteredRequest request,
            [FromServices] GetVolunteersWithPaginationFilteredDapperHandler handler,
            CancellationToken token)
        {
            var query = request.ToQuery();
            var response = await handler.Execute(query, token);
            return Ok(response);
        }

        [HttpGet("{volunteerId:guid}")]
        public async Task<ActionResult> GetById(
            [FromRoute] Guid volunteerId,
            [FromServices] GetVolunteerByIdHandler handler,
            CancellationToken token)
        {
            var query = new GetVolunteerByIdQuery(volunteerId);
            var response = await handler.Execute(query, token);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken token)
        {
            var command = request.ToCommand();
            var result = await handler.Execute(command, token);
            return result.ToResponse();
        }

        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult<Guid>> UpdateMainInfo(
            [FromServices] UpdateMainInfoHandler handler,
            [FromRoute] Guid id,
            [FromBody] UpdateMainInfoRequest request,
            CancellationToken token)
        {
            var command = request.ToCommand(id);
            var result = await handler.Execute(command, token);
            return result.ToResponse();
        }

        [HttpPut("{id:guid}/requisites")]
        public async Task<ActionResult<Guid>> UpdateRequisites(
            [FromServices] UpdateRequisitesHandler handler,
            [FromRoute] Guid id,
            [FromBody] UpdateRequisitesRequest request,
            CancellationToken token)
        {
            var command = request.ToCommand(id);
            var result = await handler.Execute(command, token);
            return result.ToResponse();
        }

        [HttpPut("{id:guid}/social-networks")]
        public async Task<ActionResult<Guid>> UpdateSocialNetworks(
            [FromServices] UpdateSocialNetworksHandler handler,
            [FromRoute] Guid id,
            [FromBody] UpdateSocialNetworksRequest request,
            CancellationToken token)
        {
            var command = request.ToCommand(id);
            var result = await handler.Execute(command, token);
            return result.ToResponse();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> Delete(
            [FromServices] DeleteVolunteerHandler handler,
            [FromRoute] Guid id,
            CancellationToken token)
        {
            var command = new DeleteVolunteerCommand(id);
            var result = await handler.Execute(command, token);
            return result.ToResponse();
        }

        [HttpPut("{id:guid}/restoring")]
        public async Task<ActionResult<Guid>> Restore(
            [FromServices] RestoreVolunteerHandler handler,
            [FromRoute] Guid id,
            CancellationToken token)
        {
            var command = new RestoreVolunteerCommand(id);
            var result = await handler.Execute(command, token);
            return result.ToResponse();
        }

        [HttpPost("{id:guid}/pet")]
        public async Task<ActionResult<Guid>> CreatePet(
           [FromRoute] Guid id,
           [FromServices] AddPetHandler handler,
           [FromBody] AddPetRequest request,
           CancellationToken token)
        {
            var command = request.ToCommand(id);
            var result = await handler.Execute(command, token);
            return result.ToResponse();
        }

        [HttpPut("{volunteerId:guid}/pet/{petId:guid}/files")]
        public async Task<ActionResult<int>> AddFile(
           [FromRoute] Guid volunteerId,
           [FromRoute] Guid petId,
           IFormFileCollection files,
           [FromServices] AddPetFilesHandler handler,
           CancellationToken token)
        {
            await using var fileProcessor = new FormFileProcessor();
            var filesDto = fileProcessor.Process(files);
            var command = new AddPetFilesCommand(volunteerId, petId, filesDto);
            var result = await handler.Execute(command, token);
            return result.ToResponse();
        }
    }
}




