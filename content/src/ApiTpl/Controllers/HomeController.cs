namespace ApiTpl.Controllers
{
    using ApiTpl.Core;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using System;

    public class HomeController : ControllerBase
    {
        private readonly AppSettings _settings;

        private readonly IWebHostEnvironment _env;

        public HomeController(IOptions<AppSettings> settings, IWebHostEnvironment env)
        {
            this._settings = settings.Value;
            this._env = env;
        }

        [Route("")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Index()
        {
            return Content($@"{ConstValue.AppVersion} Environment-{_env.EnvironmentName}; OS-{Environment.OSVersion}; Version-{Environment.Version.ToString()}");
        }

        [Route("doc")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Doc()
        {
            if (!_env.IsProduction())
            {
                return new RedirectResult("~/swagger");
            }
            else
            {
                return Content("welcome to doc page");
            }
        }
    }
}
