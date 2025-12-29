using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Common.Logging;

public static class SeriLogger
{
    public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
        (context, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .WriteTo.Console(theme: AnsiConsoleTheme.Code);

            var seqServerUrl = context.Configuration["Serilog:SeqServerUrl"];
            if (!string.IsNullOrEmpty(seqServerUrl))
            {
                configuration.WriteTo.Seq(seqServerUrl);
            }
        };
}
