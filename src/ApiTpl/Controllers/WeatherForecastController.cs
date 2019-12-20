namespace ApiTpl.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }
    }
}
