namespace ApiTpl
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Startup
    /// </summary>
    public partial class Startup
    {
        private void UseApiTpl(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (!env.IsProduction())
            {
                UseSwaggerService(app);
            }

            app.UseRequestLog();

            app.UseGlobalException();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void UseSwaggerService(IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                // c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                // c.SwaggerEndpoint("/swagger/Mgr/swagger.json", "mgr API");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 API");
            });
        }
    }
}
