using Microsoft.AspNetCore.Mvc;

namespace MultiEnvSwagger.Controllers
{
    [ApiController]
    [Route("api/Prod/[controller]")]
    [ApiExplorerSettings(GroupName = "prod")]
    public class ProdTestsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Ini endpoint khusus Prod");
    }
}
