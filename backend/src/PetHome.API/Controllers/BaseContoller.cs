using Microsoft.AspNetCore.Mvc;

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
    }
}
