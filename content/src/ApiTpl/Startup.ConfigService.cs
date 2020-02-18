namespace ApiTpl
{
    using ApiTpl.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using WebApiClient.Extensions.HttpClientFactory;

    /// <summary>
    /// Startup
    /// </summary>
    public partial class Startup
    {
        private void AddApiTpl(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            AddProDi(services);
            AddEasyCaching(services);
            AddConfigService(services);
            AddHttpClientExt(services);

            services.AddApiVersioning(x =>
            {
                x.Conventions.Add(new Microsoft.AspNetCore.Mvc.Versioning.Conventions.VersionByNamespaceConvention());
                x.ReportApiVersions = true;
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
            }).AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVV";
            });

            services.AddControllers()
                 .AddNewtonsoftJson(config =>
                 {
                     // config.SerializerSettings.ContractResolver = new DefaultContractResolver();
                     config.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                 });

            AddSwaggerService(services);
        }

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
                            EnableWriteDeepClone = false
                        };
                    }, ConstValue.AppName);
            });
        }

        private void AddHttpClientExt(IServiceCollection services)
        {
            services.AddHeadersPropagation();
            services.AddHttpApiTypedClient<Core.ApiClients.IDemoApi>(config =>
            {
                config.HttpHost = new Uri(Configuration.GetValue<string>("urls:demo"));
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() { UseProxy = false });
        }

        private IApiVersionDescriptionProvider provider;

        private void AddSwaggerService(IServiceCollection services)
        {
            provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(c =>
            {
                foreach (var item in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(item.GroupName, new OpenApiInfo
                    {
                        Version = item.GroupName,
                        Title = $"ApiTpl API",
                        Description = "ApiTpl API",
                        TermsOfService = new Uri("https://github.com/catcherwong"),
                        Contact = new OpenApiContact
                        {
                            Name = "Catcher Wong",
                            Email = "catcher_hwq@outlook.com",
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Apache License, Version 2.0",
                            Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0")
                        }
                    });
                }

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var versions = apiDesc.CustomAttributes()
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });

                c.OperationFilter<RemoveVersionParameterOperationFilter>();
                c.DocumentFilter<SetVersionInPathDocumentFilter>();

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
    }
}
