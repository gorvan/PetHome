using Microsoft.AspNetCore.Mvc;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Models;
using PetHome.Shared.Core.Extensions;
using PetHome.Species.Application.Contracts;
using PetHome.Species.Application.SpeciesManagement.Commands.Delete;
using PetHome.Species.Application.SpeciesManagement.Queries.GetBreeds;
using PetHome.Species.Application.SpeciesManagement.Queries.GetSpecies;
using PetHome.Shared.Framework.Controllers;
using PetHome.Shared.SharedKernel.Authorization;

namespace PetHome.Species.Presentation.Controllers
{
    public class SpeciesController : ApplicationContoller
    {
        [Permission(Permissions.Participant.ReadParticipant)]
        [HttpGet]
        public async Task<ActionResult> Get(
            [FromQuery] GetSpeciesWithPaginationRequest request,
            [FromServices] GetSpeciesWithPaginationHandler handler,
            CancellationToken token)
        {
            var query = request.ToQuery();
            var response = await handler.Execute(query, token);
            return Ok(response);
        }

        [Permission(Permissions.Participant.ReadParticipant)]
        [HttpGet("{speciesId:Guid}/breeds")]
        public async Task<ActionResult<PagedList<BreedDto>>> GetBreeds(
            [FromRoute] Guid speciesId,
            [FromQuery] GetBreedWithPaginationRequest request,
            [FromServices] GetBreedsWithPaginationHandler handler,
            CancellationToken token)
        {
            var query = request.ToQuery(speciesId);
            var response = await handler.Execute(query, token);
            if (response.IsFailure)
            {
                return response.ToResponse();
            }
            return Ok(response);
        }

        [Permission(Permissions.Volunteers.DeleteVolunteer)]
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Guid>> Delete(
            [FromRoute] Guid id,
            [FromServices] DeleteSpeciesHandler handler,
            CancellationToken token)
        {
            var command = new DeleteSpeciesCommand(id);
            var result = await handler.Execute(command, token);
            return result.ToResponse();

        }
    }
}
