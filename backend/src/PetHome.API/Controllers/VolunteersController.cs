using Microsoft.AspNetCore.Mvc;
using PetHome.API.Extensions;
using PetHome.Application.Volunteers.CreateVolunteer;

namespace PetHome.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteersController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken token)
        {
            var result = await handler.Execute(request, token);

            return result.ToResponse<Guid>();
        }
    }
}
