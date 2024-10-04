using Microsoft.AspNetCore.Mvc;
using PetHome.API.Contracts;
using PetHome.API.Extensions;
using PetHome.Application.Dtos;
using PetHome.Application.Models;
using PetHome.Application.SpeciesManagement.Commands.Delete;
using PetHome.Application.SpeciesManagement.Queries.GetBreeds;
using PetHome.Application.SpeciesManagement.Queries.GetSpecies;
using PetHome.Application.VolunteersManagement.Commands.Delete;

namespace PetHome.API.Controllers
{
    public class SpeciesController : ApplicationContoller
    {
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

        [HttpGet("{speciesId:Guid}/breeds")]
        public async Task<ActionResult<PagedList<BreedDto>>> GetBreeds(
            [FromRoute] Guid speciesId,
            [FromQuery] GetBreedWithPaginationRequest request,
            [FromServices] GetBreedsWithPaginationHandler handler,
            CancellationToken token)
        {
            var query = request.ToQuery(speciesId);
            var response = await handler.Execute(query, token);
            if(response.IsFailure)
            {
                return response.ToResponse();
            }
            return Ok(response);
        }

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
