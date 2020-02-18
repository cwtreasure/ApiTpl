namespace ApiTpl.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ApiTplBaseController : ControllerBase
    {
        protected string TraceId => HttpContext.Items.TryGetValue("traceId", out var rId) ? rId.ToString() : System.Guid.NewGuid().ToString("N");

        protected string UserIp => HttpContext.Items.TryGetValue("userIp", out var uIp) ? uIp.ToString() : HttpContext.Connection.RemoteIpAddress?.ToString();
    }
}
