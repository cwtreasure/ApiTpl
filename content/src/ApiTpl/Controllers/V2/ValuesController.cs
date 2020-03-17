namespace ApiTpl.Controllers.V2
{
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/values")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "v2";
        }
    }
}
