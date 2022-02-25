using Datadog.Trace;
using Datadog.Trace.Configuration;
using HexaEmployee.Api.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;

namespace HexaEmployee.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class DatadogExtension
    {
        public static IServiceCollection AddDataDog(this IServiceCollection services)
        {
            // This vars must be read from IConfiguration root because they are shared with Datadog agent.
            return services.AddSingleton(provider => provider
                .GetRequiredService<IConfiguration>()
                .Get<DatadogSettings>());
        }

        public static IApplicationBuilder UseDatadogAPMTraces(this IApplicationBuilder app)
        {
            var logger = app.ApplicationServices
                .GetService<ILogger<DatadogSettings>>();
            var datadogSettings = app.ApplicationServices
                .GetRequiredService<DatadogSettings>();

            if (!datadogSettings.DD_APM_ENABLED)
            {
                logger.LogInformation("Datadog APM is not enabled.");
                return app;
            }

            logger.LogInformation("Start Datadog APM Traces. Env: {0}", datadogSettings.DD_ENV);

            try
            {
                Tracer.Instance = ConfigureTracing(datadogSettings);
                return app;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error while starting APM.");

                if (!datadogSettings.DD_ContinueOnError)
                {
                    throw;
                }

                return app;
            }
        }

        private static Tracer ConfigureTracing(DatadogSettings datadogSettings)
        {
            GlobalSettings.SetDebugEnabled(datadogSettings.DD_APM_ENABLED);

            var settings = TracerSettings.FromDefaultSources();

            settings.Environment = datadogSettings.DD_ENV;
            settings.ServiceName = datadogSettings.DD_SERVICE;
            settings.ServiceVersion = datadogSettings.DD_VERSION;
            settings.AgentUri = new Uri($"http://{datadogSettings.DD_AGENT_HOST}:{datadogSettings.DD_AGENT_HOST_PORT}/");

            settings.TraceEnabled = datadogSettings.DD_APM_ENABLED;
            settings.AnalyticsEnabled = datadogSettings.DD_TRACE_ANALYTICS_ENABLED;
            settings.RuntimeMetricsEnabled = datadogSettings.DD_RUNTIME_METRICS_ENABLED;
            settings.TracerMetricsEnabled = datadogSettings.DD_APM_METRICS_ENABLED;
            settings.StartupDiagnosticLogEnabled = datadogSettings.DD_LOGS_ENABLED;

            foreach (var tag in datadogSettings.DD_TAGS)
            {
                settings.HeaderTags.Add(tag.Key, tag.Value);
            }

            foreach (var tag in datadogSettings.DD_GLOBAL_TAGS)
            {
                settings.GlobalTags.Add(tag.Key, tag.Value);
            }

            return new Tracer(settings);
        }
    }
}
