using Microsoft.AspNetCore.Mvc;

namespace MultiEnvSwagger.Controllers
{
    [ApiController]
    [Route("api/Dev/[controller]")]
    [ApiExplorerSettings(GroupName = "dev")]
    public class DevTestsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Ini endpoint khusus Dev");
    }
}
