namespace ApiTpl.Controllers
{
    using ApiTpl.Core;
    using ApiTpl.Service.UserSvc;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    public class ApiTplBaseController : ControllerBase
    {
        protected string TraceId => HttpContext.Items.TryGetValue("traceId", out var rId) ? rId.ToString() : System.Guid.NewGuid().ToString("N");

        protected string UserIp => HttpContext.Items.TryGetValue("userIp", out var uIp) ? uIp.ToString() : HttpContext.Connection.RemoteIpAddress?.ToString();
    }
}
