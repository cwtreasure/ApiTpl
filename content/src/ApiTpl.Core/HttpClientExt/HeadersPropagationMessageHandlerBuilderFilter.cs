namespace ApiTpl.Core.HttpClientExt
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Http;

    public class HeadersPropagationMessageHandlerBuilderFilter : IHttpMessageHandlerBuilderFilter
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HeadersPropagationMessageHandlerBuilderFilter(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return (builder) =>
            {
                next(builder);

                builder.AdditionalHandlers.Add(new HeadersPropagationDelegatingHandler(httpContextAccessor));
            };
        }
    }
}
