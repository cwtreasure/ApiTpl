namespace ApiTpl
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Startup
    /// </summary>
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AddApiTpl(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UseApiTpl(app, env);
        }
    }
}
