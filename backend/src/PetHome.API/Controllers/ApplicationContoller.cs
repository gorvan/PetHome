using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PetHome.API.Response;

namespace PetHome.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApplicationContoller : ControllerBase
    {
        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            var envelope = Envelope.Ok(value);
            return base.Ok(envelope);
        }
    }
}
