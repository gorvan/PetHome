using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHome.API.Extensions;
using PetHome.API.Processors;
using PetHome.Application.Volunteers.AddPet;
using PetHome.Application.Volunteers.AddPetFiles;
using PetHome.Application.Volunteers.Create;
using PetHome.Application.Volunteers.Delete;
using PetHome.Application.Volunteers.Restore;
using PetHome.Application.Volunteers.Shared;
using PetHome.Application.Volunteers.UpdateMainInfo;
using PetHome.Application.Volunteers.UpdateRequisites;
using PetHome.Application.Volunteers.UpdateSocialNetworks;

namespace PetHome.API.Controllers
{

    public class VolunteersController : BaseContoller
    {
        public VolunteersController(ILogger<VolunteersController> logger)
            : base(logger)
        {
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken token)
        {
            _logger.LogInformation("Create volunteer request");

            var result = await handler.Execute(request, token);
            return result.ToResponse<Guid>();
        }

        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult<Guid>> UpdateMainInfo(
            [FromServices] UpdateMainInfoHandler handler,
            [FromRoute] Guid id,
            [FromBody] UpdateMainInfoDto updateMainInfoDto,
            [FromServices] IValidator<UpdateMainInfoRequest> validator,
            CancellationToken token)
        {
            var request = new UpdateMainInfoRequest(id, updateMainInfoDto);

            var validateResult =
                await validator.ValidateAsync(request, token);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToErrorValidationResponse();
            }

            _logger.LogInformation("Update volunteer request");
            var result = await handler.Execute(request, token);
            return result.ToResponse();
        }

        [HttpPut("{id:guid}/requisites")]
        public async Task<ActionResult<Guid>> UpdateRequisites(
            [FromServices] UpdateRequisitesHandler handler,
            [FromRoute] Guid id,
            [FromBody] List<RequisiteDto> requisiteDtos,
            [FromServices] IValidator<UpdateRequisitesRequest> validator,
            CancellationToken token)
        {
            var request = new UpdateRequisitesRequest(id, requisiteDtos);

            var validateResult =
                await validator.ValidateAsync(request, token);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToErrorValidationResponse();
            }

            _logger.LogInformation("Update volunteer requisites request");
            var result = await handler.Execute(request, token);
            return result.ToResponse();
        }

        [HttpPut("{id:guid}/social-networks")]
        public async Task<ActionResult<Guid>> UpdateSocialNetworks(
            [FromServices] UpdateSocialNetworksHandler handler,
            [FromRoute] Guid id,
            [FromBody] List<SocialNetworkDto> socialNetworkDtos,
            [FromServices] IValidator<UpdateSocialNetworksRequest> validator,
            CancellationToken token)
        {
            var request = new UpdateSocialNetworksRequest(id, socialNetworkDtos);

            var validateResult =
                await validator.ValidateAsync(request, token);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToErrorValidationResponse();
            }

            _logger.LogInformation("Update volunteer social networks request");
            var result = await handler.Execute(request, token);
            return result.ToResponse();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> Delete(
            [FromServices] DeleteVolunteerHandler handler,
            [FromRoute] Guid id,
            CancellationToken token)
        {
            var request = new DeleteVolunteerRequest(id);

            _logger.LogInformation("Delete volunteer request");

            var result = await handler.Execute(request, token);
            return result.ToResponse();
        }

        [HttpPut("{id:guid}/restoring")]
        public async Task<ActionResult<Guid>> Restore(
            [FromServices] RestoreVolunteerHandler handler,
            [FromRoute] Guid id,
            CancellationToken token)
        {
            var request = new RestoreVolunteerRequest(id);

            _logger.LogInformation("Restore volunteer request");

            var result = await handler.Execute(request, token);
            return result.ToResponse();
        }

        [HttpPost("{id:guid}/pet")]
        public async Task<ActionResult<Guid>> CreatePet(
           [FromRoute] Guid id,
           [FromServices] AddPetHandler handler,
           [FromBody] AddPetRequest request,
           CancellationToken token)
        {
            var command = new AddPetCommand(
                id,
                request.Nickname,
                request.Description,
                request.Color,
                request.Health,
                request.Address,
                request.Phone,
                request.Requisites,
                request.BirthDay,
                request.IsNeutered,
                request.IsVaccinated,
                request.HelpStatus,
                request.Weight,
                request.Height);

            _logger.LogInformation("Create pet request");

            var result = await handler.Execute(command, token);
            return result.ToResponse<Guid>();
        }

        [HttpPut("{volunteerid:guid}/pet/{petid:guid}/files")]
        public async Task<ActionResult<int>> AddFile(
           [FromRoute] Guid volunteerid,
           [FromRoute] Guid petid,
           IFormFileCollection files,
           [FromServices] AddPetFilesHandler handler,
           CancellationToken token)
        {
            await using var fileProcessor = new FormFileProcessor();
            var filesDto = fileProcessor.Process(files);

            var command = new AddPetFilesCommand(volunteerid, petid, filesDto);

            _logger.LogInformation("Add pet photos request");

            var result = await handler.Execute(command, token);

            return result.ToResponse<int>();
        }
    }
}


