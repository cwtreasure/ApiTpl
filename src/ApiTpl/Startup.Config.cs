namespace ApiTpl
{
    using ApiTpl.Core;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Startup
    /// </summary>
    public partial class Startup
    {
        private void AddConfigService(IServiceCollection services)
        {
            var settingConfigSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(settingConfigSection);
        }

        private void AddProDi(IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(Service.EmptyService), typeof(Data.DapperRepositoryBase))

                    // repo
                    .AddClasses(classes => classes.AssignableTo<IBaseRepo>())
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime()

                    // service
                    .AddClasses(classes => classes.AssignableTo<IBaseService>())
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime());
        }

        private void AddEasyCaching(IServiceCollection services)
        {
            services.AddEasyCaching(options =>
            {
                options.UseInMemory(
                    config =>
                    {
                        config.MaxRdSecond = 0;
                        config.EnableLogging = false;
                        config.DBConfig = new EasyCaching.InMemory.InMemoryCachingOptions
                        {
                            EnableReadDeepClone = false,
                            EnableWriteDeepClone = false,
                            SizeLimit = 100000
                        };
                    }, ConstValue.AppName);
            });
        }

        private void AddHttpClientExt(IServiceCollection services)
        {
            services.AddHeadersPropagation();
        }

        private void AddSwaggerService(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0.0",
                    Title = $"v1 API",
                    Description = "v1 API",
                    TermsOfService = new Uri("https://www.baidu.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Catcher Wong",
                        Email = "catcher_hwq@outlook.com",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "CW License",
                        Url = new Uri("https://www.baidu.com")
                    }
                });

                /*c.SwaggerDoc("Mgr", new OpenApiInfo
                {
                    Version = "v1.0.0",
                    Title = $"Mgr API",
                    Description = "Mgr API",
                    TermsOfService = new Uri("https://www.baidu.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Catcher Wong",
                        Email = "catcher_hwq@outlook.com",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "CW License",
                        Url = new Uri("https://www.baidu.com")
                    }
                });*/

                var projectName = Assembly.GetExecutingAssembly().GetName().Name;

                var webXmlFile = $"{projectName}.xml";
                var webXmlPath = Path.Combine(AppContext.BaseDirectory, webXmlFile);
                c.IncludeXmlComments(webXmlPath);

                var coreXmlFile = $"{projectName}.Core.xml";
                var coreXmlPath = Path.Combine(AppContext.BaseDirectory, coreXmlFile);
                c.IncludeXmlComments(coreXmlPath);

                var serviceXmlFile = $"{projectName}.Service.xml";
                var serviceXmlPath = Path.Combine(AppContext.BaseDirectory, serviceXmlFile);
                c.IncludeXmlComments(serviceXmlPath);
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

        private void AddApiTpl(IServiceCollection services)
        {
            AddProDi(services);
            AddEasyCaching(services);
            AddConfigService(services);

            AddSwaggerService(services);
        }
    }
}
