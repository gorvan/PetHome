using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PetHome.Shared.Core.Response;

namespace PetHome.Shared.Framework.Controllers
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
