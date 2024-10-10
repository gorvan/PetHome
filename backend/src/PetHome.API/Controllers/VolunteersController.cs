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
using PetHome.Application.VolunteersManagement.Commands.PetManagement.DeletePet;
using PetHome.Application.VolunteersManagement.Commands.PetManagement.FullDeletePet;
using PetHome.Application.VolunteersManagement.Commands.PetManagement.SetMainPetPhoto;
using PetHome.Application.VolunteersManagement.Commands.PetManagement.UpdateFiles;
using PetHome.Application.VolunteersManagement.Commands.PetManagement.UpdatePet;
using PetHome.Application.VolunteersManagement.Commands.PetManagement.UpdatePetHelpStatus;
using PetHome.Application.VolunteersManagement.Commands.Restore;
using PetHome.Application.VolunteersManagement.Commands.UpdateMainInfo;
using PetHome.Application.VolunteersManagement.Commands.UpdateRequisites;
using PetHome.Application.VolunteersManagement.Commands.UpdateSocialNetworks;
using PetHome.Application.VolunteersManagement.Queries.GetAllPets;
using PetHome.Application.VolunteersManagement.Queries.GetPetById;
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

        [HttpPut("{volunteerId:guid}/pet/{petId:guid}/edit")]
        public async Task<ActionResult<Guid>> UpdatePet(
            [FromServices] UpdatePetHandler handler,
            [FromRoute] Guid volunteerId,
            [FromRoute] Guid petId,
            [FromBody] UpdatePetRequest request,
            CancellationToken token)
        {
            var command = request.ToCommand(volunteerId, petId);
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

        [HttpPut("{volunteerId:guid}/pet/{petId:guid}/files/edit")]
        public async Task<ActionResult<int>> UpdateFile(
           [FromRoute] Guid volunteerId,
           [FromRoute] Guid petId,
           IFormFileCollection files,
           [FromServices] UpdateFilesHandler handler,
           CancellationToken token)
        {
            await using var fileProcessor = new FormFileProcessor();
            var filesDto = fileProcessor.Process(files);
            var command = new UpdateFilesCommand(volunteerId, petId, filesDto);
            var result = await handler.Execute(command, token);
            return result.ToResponse();
        }

        [HttpPut("{volunteerId:guid}/pet/{petId:guid}/help_status")]
        public async Task<ActionResult<Guid>> UpdateHelpStatus(
           [FromRoute] Guid volunteerId,
           [FromRoute] Guid petId,
           [FromBody] UpdatePetHelpStatusRequest request,
           [FromServices] UpdatePetHelpStatusHandler handler,
           CancellationToken token)
        {
            var command = request.ToCommand(volunteerId, petId);
            var result = await handler.Execute(command, token);
            return result.ToResponse();
        }

        [HttpDelete("{volunteerId:guid}/pet/{petId:guid}")]
        public async Task<ActionResult<Guid>> DeletePet(
            [FromServices] DeletePetHandler handler,
            [FromRoute] Guid volunteerId,
            [FromRoute] Guid petId,
            CancellationToken token)
        {
            var command = new DeletePetCommand(volunteerId, petId);
            var result = await handler.Execute(command, token);
            return result.ToResponse();
        }

        [HttpDelete("{volunteerId:guid}/pet/{petId:guid}/full")]
        public async Task<ActionResult<Guid>> FullDeletePet(
            [FromServices] FullDeletePetHandler handler,
            [FromRoute] Guid volunteerId,
            [FromRoute] Guid petId,
            CancellationToken token)
        {
            var command = new FullDeletePetCommand(volunteerId, petId);
            var result = await handler.Execute(command, token);
            return result.ToResponse();
        }

        [HttpGet("pets")]
        public async Task<ActionResult> GetAllPets(
            [FromServices] GetPetsWithPaginationFilterdHandler handler,            
            [FromQuery] GetPetsWithPaginationFilteredRequest request,
            CancellationToken token)
        {
            var query = request.ToQuery();
            var result = await handler.Execute(query, token);
            return Ok(result);
        }

        [HttpGet("pet/{petId:guid}")]
        public async Task<ActionResult> GetPetById(
            [FromRoute] Guid petId,
            [FromServices] GetPetByIdHandler handler,           
            CancellationToken token)
        {
            var query = new GetPetByIdQuery(petId);
            var result = await handler.Execute(query, token);
            return Ok(result);
        }

        [HttpPut("{volunteerId:guid}/pet/{petId:guid}/photo_set_main")]
        public async Task<ActionResult<Guid>> SetMainPetPhoto(
           [FromRoute] Guid volunteerId,
           [FromRoute] Guid petId,
           [FromBody] SetMainPetPhotoRequest request,
           [FromServices] SetMainPetPhotoHandler handler,
           CancellationToken token)
        {
            var command = request.ToCommand(volunteerId, petId);
            var result = await handler.Execute(command, token);
            return result.ToResponse();
        }
    }
}




