using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PetHome.API.Response;

namespace PetHome.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseContoller : ControllerBase
    {
        protected ILogger<BaseContoller> _logger;
        public BaseContoller(ILogger<BaseContoller> logger)
        {
            _logger = logger;
        }

        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            var envelope = Envelope.Ok(value);
            return base.Ok(envelope);
        }
    }
}
