namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using ApiTpl.Core.HttpClientExt;
    using Microsoft.Extensions.Http;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHeadersPropagation(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddTransient<HeadersPropagationDelegatingHandler>();
            services.AddSingleton<IHttpMessageHandlerBuilderFilter, HeadersPropagationMessageHandlerBuilderFilter>();
            services.AddHttpClient();

            return services;
        }
    }
}
