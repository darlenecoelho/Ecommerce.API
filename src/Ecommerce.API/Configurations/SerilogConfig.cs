using Serilog;
using Serilog.Events;

namespace Ecommerce.API.Configurations;

public static class SerilogConfig
{
    public static void AddSerilog(WebApplicationBuilder builder)
    {
        Action<HostBuilderContext, LoggerConfiguration> configureLogger = (cfg, logConfig) => logConfig
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithCorrelationId()
            .Filter.ByExcluding(p => p.MessageTemplate.Text.Contains("System"))
            .Filter.ByExcluding(p => p.MessageTemplate.Text.Contains("Health"))
            .Filter.ByExcluding(p => p.MessageTemplate.Text.Contains("Microsoft"))
            .Filter.ByExcluding(p => CheckSourceContextEquals(p))
            .WriteTo.Async(wt => wt.Console());

        builder.Host.UseSerilog(configureLogger);
    }

    private static bool CheckSourceContextEquals(LogEvent logEvent)
    {
        logEvent.Properties.TryGetValue("SourceContext", out var source);

        return source.ToString().Contains("Microsoft.EntityFrameworkCore.Infrastructure") ||
               source.ToString().Contains("Microsoft.Hosting.Lifetime") ||
               source.ToString().Contains("Microsoft.EntityFrameworkCore.Infrastructure");
    }
}
