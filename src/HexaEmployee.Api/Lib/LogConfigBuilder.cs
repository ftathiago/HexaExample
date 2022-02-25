using HexaEmployee.Api.Extensions;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace HexaEmployee.Api.Lib
{
    [ExcludeFromCodeCoverage]
    public class LogConfigBuilder
    {
        private readonly IConfigurationRoot _configuration;
        private readonly string _environment;

        public LogConfigBuilder(IConfigurationRoot configuration, string environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public static void AutoWire()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .SetupSource()
                .Build();
            var log = new LogConfigBuilder(configuration, environment);
            log.Build();
        }

        public void Build() =>
            Log.Logger = GetLoggerConfiguration();

        private ILogger GetLoggerConfiguration()
        {
            var logConfigBuilder = new LoggerConfiguration()
                .Enrich.WithExceptionData()
                .Enrich.WithMachineName()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(_configuration)
                .WriteTo.Console(new RenderedCompactJsonFormatter());

            return WriteLogOnFile(logConfigBuilder)
                .CreateLogger();
        }

        private LoggerConfiguration WriteLogOnFile(LoggerConfiguration configuration) =>
            _configuration.GetValue<bool>("WriteLogFile")
            ? configuration.WriteTo.File(new JsonFormatter(), GetLogFileName())
            : configuration;

        private string GetLogFileName() => $"{GetLogIdentifier()}.log";

        private string GetLogIdentifier()
        {
            var logBaseName = Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-");
            var logByDay = $"{DateTime.UtcNow:yyyy-MM-dd}";
            return $"{logBaseName}-{_environment}-{logByDay}";
        }
    }
}
