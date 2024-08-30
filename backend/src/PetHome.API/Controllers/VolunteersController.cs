using Microsoft.AspNetCore.Mvc;
using PetHome.API.Extensions;
using PetHome.Application.Volunteers.CreateVolunteer;

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
    }
}


