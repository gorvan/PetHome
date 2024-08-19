using Microsoft.AspNetCore.Mvc;
using PetHome.Application.Volunteers.CreateVolunteer;

namespace PetHome.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteersController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateVolunteerRequest request,
            [FromServices] CreateVolunteerHandler handler,
            CancellationToken token)
        {
            var result = await handler.Execute(request, token);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result);
        }
    }


}
