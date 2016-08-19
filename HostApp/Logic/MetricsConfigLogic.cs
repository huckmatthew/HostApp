using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostApp.Core.Constants;
using HostApp.Core.DTO;
using HostApp.Core.Extensions;
using HostApp.Core.Interfaces;
using Metrics;
using Owin;
using Owin.Metrics;
using IMetricsConfigLogic = HostApp.Interfaces.IMetricsConfigLogic;

namespace HostApp.Logic
{
    public class MetricsConfigLogic : IMetricsConfigLogic
    {
        private const string MetricsRequestErrorMeter = "{0} Errors";
        private const string MetricsAllRequestsTimer = "{0} All Request Timer";
        private const string MetrricsActiveRequestCounter = "{0} All Active Requests";
        public void Config(IAppBuilder appBuilder, IApplicationConfiguration appConfig)
        {
            ConfigMetrics(appBuilder, appConfig);
            var hostappname = "{0}.{1}".FormatWith(appConfig.HostName, appConfig.ApplicationName);
            Metric.Config
                .WithOwin(mid => appBuilder.Use(mid), conf => conf
                    .WithRequestMetricsConfig(c =>
                        c.WithErrorsMeter(string.Format(MetricsRequestErrorMeter, hostappname))
                         .WithRequestTimer(string.Format(MetricsAllRequestsTimer, hostappname))
                         .WithActiveRequestCounter(string.Format(MetrricsActiveRequestCounter, hostappname))
                    ));

        }

        private void ConfigMetrics(IAppBuilder appBuilder, IApplicationConfiguration appConfig)
        {
            var metricConfig = GetIndividualMetric(appConfig.MetricConfiguration, MetricsConstants.Metrics);
            var jsonConfig = GetIndividualMetric(appConfig.MetricConfiguration, MetricsConstants.JSON);
            var healthConfig = GetIndividualMetric(appConfig.MetricConfiguration, MetricsConstants.Health);
            var pingConfig = GetIndividualMetric(appConfig.MetricConfiguration, MetricsConstants.Ping);
            var textConfig = GetIndividualMetric(appConfig.MetricConfiguration, MetricsConstants.Text);
            Metric.Config
                .WithOwin(mid => appBuilder.Use(mid), conf => conf
                    .WithMetricsEndpoint(endpointConfig => endpointConfig
                        .MetricsEndpoint(endpoint: metricConfig.url, enabled: metricConfig.enabled)
                        .MetricsJsonEndpoint(endpoint: jsonConfig.url, enabled: jsonConfig.enabled)
                        .MetricsHealthEndpoint(endpoint: healthConfig.url, enabled: healthConfig.enabled)
                        .MetricsPingEndpoint(endpoint: pingConfig.url, enabled: pingConfig.enabled)
                        .MetricsTextEndpoint(endpoint: textConfig.url, enabled: textConfig.enabled)));
        }

        private MetricsSettingDTO GetIndividualMetric(IEnumerable<MetricsSettingDTO> metric, string metricName)
        {
            var data = metric.FirstOrDefault(d => d.name.Equals(metricName, StringComparison.InvariantCultureIgnoreCase)) ??
                       new MetricsSettingDTO { enabled = false, name = metricName, url = metricName };

            return data;
        }
    }
}
