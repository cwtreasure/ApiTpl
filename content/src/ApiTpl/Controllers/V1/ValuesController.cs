namespace ApiTpl.Controllers.V1
{
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/values")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "v1";
        }
    }
}
