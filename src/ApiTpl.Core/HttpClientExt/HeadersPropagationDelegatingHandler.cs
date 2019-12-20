namespace ApiTpl.Core.HttpClientExt
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class HeadersPropagationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;

        public HeadersPropagationDelegatingHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string traceId;

            if (_accessor.HttpContext.Request.Headers.TryGetValue("traceId", out var tId))
            {
                traceId = tId.ToString();
            }
            else
            {
                traceId = Guid.NewGuid().ToString("N");
            }

            if (!request.Headers.Contains("traceId"))
            {
                request.Headers.TryAddWithoutValidation("traceId", traceId);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
