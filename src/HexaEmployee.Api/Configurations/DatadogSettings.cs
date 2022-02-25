using System.Collections.Generic;

namespace HexaEmployee.Api.Configurations
{
    public class DatadogSettings
    {
        private const string EmptyValue = "none";

        public DatadogSettings()
        {
            DD_GLOBAL_TAGS = new Dictionary<string, string>();
            DD_TAGS = new Dictionary<string, string>();
        }

        /// <summary>
        /// By default this should be the cluster name.
        /// Ex: aks-ambevtech-brewtech-prod
        /// Ex: aks-ambevtech-brewtech-nonprod
        /// Default value "none"
        /// </summary>
        public string DD_ENV { get; set; } = EmptyValue;

        /// <summary>
        /// By default it must be a concatenated.
        /// namespace + deploy
        /// Ex: manutencao-nonprod-system-backend
        /// Ex: manutencao-qa-system-backend
        /// Ex: manutencao-dev-system-backend
        /// Ex: manutencao-prod-system-backend
        /// Default value "none"
        /// </summary>
        public string DD_SERVICE { get; set; } = EmptyValue;

        public string DD_VERSION { get; set; } = "1.0.0";

        /// <summary>
        /// Included deploy.yaml or Host IP.
        /// EX: Deploy
        /// - name: DD_AGENT_HOST
        /// valueFrom:
        ///     fieldRef:
        ///       apiVersion: v1
        ///       fieldPath: status.hostIP
        /// </summary>
        public string DD_AGENT_HOST { get; set; } = "localhost";

        /// <summary>
        /// Default value "8126"
        /// </summary>
        public int DD_AGENT_HOST_PORT { get; set; } = 8126;

        /// <summary>
        /// TraceEnabled
        /// </summary>
        public bool DD_APM_ENABLED { get; set; } = true;

        /// <summary>
        /// AnalyticsEnabled
        /// </summary>
        public bool DD_TRACE_ANALYTICS_ENABLED { get; set; } = true;

        /// <summary>
        /// RuntimeMetricsEnabled
        /// </summary>
        public bool DD_RUNTIME_METRICS_ENABLED { get; set; } = true;

        /// <summary>
        /// TracerMetricsEnabled
        /// </summary>
        public bool DD_APM_METRICS_ENABLED { get; set; } = true;

        /// <summary>
        /// StartupDiagnosticLogEnabled
        /// </summary>
        public bool DD_LOGS_ENABLED { get; set; } = true;

        /// <summary>
        /// HeaderTags
        /// </summary>
        public Dictionary<string, string> DD_TAGS { get; set; }

        /// <summary>
        /// GlobalTags
        /// </summary>
        public Dictionary<string, string> DD_GLOBAL_TAGS { get; set; }

        public bool DD_ContinueOnError { get; set; } = true;
    }
}
