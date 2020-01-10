namespace ApiTpl.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<WeatherForecastController>();
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("info here {Name}", "catcher");
            _logger.LogWarning("warning here {Name}", "catcher");
            _logger.LogError(new System.Exception("exce"), "some error here");

            return new[] { "value1", "value2" };
        }
    }
}
