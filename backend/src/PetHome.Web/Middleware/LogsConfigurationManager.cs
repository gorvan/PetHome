﻿using Serilog;
using Serilog.Events;

namespace PetHome.Web.Middleware
{
    public class LogsConfigurationManager
    {
        public static void ConfigureLogging(WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .Enrich.WithEnvironmentName()
                .Enrich.WithThreadId()
                .WriteTo.Seq(
                        builder.Configuration.GetConnectionString("Seq")
                        ?? throw new ArgumentNullException("Seq"))
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
                .CreateLogger();
        }
    }

    public static class LoggingManagerExtensions
    {
        public static void ConfigureLogging(this WebApplicationBuilder builder) =>
            LogsConfigurationManager.ConfigureLogging(builder);
    }
}
