namespace ApiTpl
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Serilog.Events;

    public class Program
    {
        public static void Main(string[] args)
        {
            var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] [{AppName}] [{SourceContext}] [{UserIp}] [{cTraceId}] {Message}{NewLine}{Exception}";

            // with default serilog configuration
            // CW.Middleware.Serilog.SerilogHosting.Run(() => CreateHostBuilder(args));
            //
            // with custom serilog configuration
            CW.Middleware.Serilog.SerilogHosting.Run(
                () => CreateHostBuilder(args),
                (config) =>
                {
                    config.Enrich.FromLogContext()
                          .Enrich.WithProperty("AppName", "ApiTpl")
                          .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                          .MinimumLevel.Override("System", LogEventLevel.Warning)
                          .MinimumLevel.Debug()
                          .WriteTo.Console(
                              outputTemplate: outputTemplate)
                          /*.WriteTo.File(
                              path: "logs/ApiTpl.log",
                              outputTemplate: outputTemplate,
                              rollingInterval: RollingInterval.Day,
                              retainedFileCountLimit: 5,
                              encoding: System.Text.Encoding.UTF8)*/
                          ;
                });
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
#if DEBUG
                        .UseUrls("http://*:10001")
#endif

                        .UseJexusIntegration();
                });
    }
}
