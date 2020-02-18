namespace ApiTpl.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiVersionNeutral]
    [ApiController]
    [Route("api/health")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping() => Ok();
    }
}
