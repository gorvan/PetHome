using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHome.API.Extensions;
using PetHome.API.Processors;
using PetHome.Application.Volunteers.AddPet;
using PetHome.Application.Volunteers.Shared;
using PetHome.Application.Volunteers.UpdateMainInfo;
using PetHome.Application.VolunteersManagement.Create;
using PetHome.Application.VolunteersManagement.Delete;
using PetHome.Application.VolunteersManagement.PetManagement.AddPet;
using PetHome.Application.VolunteersManagement.PetManagement.AddPetFiles;
using PetHome.Application.VolunteersManagement.Restore;
using PetHome.Application.VolunteersManagement.UpdateMainInfo;
using PetHome.Application.VolunteersManagement.UpdateRequisites;
using PetHome.Application.VolunteersManagement.UpdateSocialNetworks;

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
            [FromBody] CreateVolunteerCommand request,
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
            [FromBody] UpdateMainInfoRequest request,
            [FromServices] IValidator<UpdateMainInfoCommand> validator,
            CancellationToken token)
        {
            var command = new UpdateMainInfoCommand(
                id,
                request.FullName,
                request.Email,
                request.Phone,
                request.Description);

            _logger.LogInformation("Update volunteer request");

            var result = await handler.Execute(command, token);

            return result.ToResponse();
        }

        [HttpPut("{id:guid}/requisites")]
        public async Task<ActionResult<Guid>> UpdateRequisites(
            [FromServices] UpdateRequisitesHandler handler,
            [FromRoute] Guid id,
            [FromBody] List<RequisiteDto> requisiteDtos,
            [FromServices] IValidator<UpdateRequisitesCommand> validator,
            CancellationToken token)
        {
            var request = new UpdateRequisitesCommand(id, requisiteDtos);

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
            [FromServices] IValidator<UpdateSocialNetworksCommand> validator,
            CancellationToken token)
        {
            var request = new UpdateSocialNetworksCommand(id, socialNetworkDtos);

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
            [FromServices] IValidator<DeleteVolunteerCommand> validator,
            CancellationToken token)
        {
            var request = new DeleteVolunteerCommand(id);

            var validateResult =
                await validator.ValidateAsync(request, token);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToErrorValidationResponse();
            }

            _logger.LogInformation("Delete volunteer request");

            var result = await handler.Execute(request, token);
            return result.ToResponse();
        }

        [HttpPut("{id:guid}/restoring")]
        public async Task<ActionResult<Guid>> Restore(
            [FromServices] RestoreVolunteerHandler handler,
            [FromRoute] Guid id,
            [FromServices] IValidator<RestoreVolunteerCommand> validator,
            CancellationToken token)
        {
            var request = new RestoreVolunteerCommand(id);

            var validateResult =
               await validator.ValidateAsync(request, token);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToErrorValidationResponse();
            }

            _logger.LogInformation("Restore volunteer request");

            var result = await handler.Execute(request, token);
            return result.ToResponse();
        }

        [HttpPost("{id:guid}/pet")]
        public async Task<ActionResult<Guid>> CreatePet(
           [FromRoute] Guid id,
           [FromServices] AddPetHandler handler,
           [FromBody] AddPetRequest request,
           [FromServices] IValidator<AddPetCommand> validator,
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

            var validateResult =
              await validator.ValidateAsync(command, token);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToErrorValidationResponse();
            }

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
           [FromServices] IValidator<AddPetFilesCommand> validator,
           CancellationToken token)
        {
            await using var fileProcessor = new FormFileProcessor();
            var filesDto = fileProcessor.Process(files);

            var command = new AddPetFilesCommand(volunteerid, petid, filesDto);

            var validateResult =
              await validator.ValidateAsync(command, token);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToErrorValidationResponse();
            }

            _logger.LogInformation("Add pet photos request");

            var result = await handler.Execute(command, token);

            return result.ToResponse<int>();
        }
    }
}


