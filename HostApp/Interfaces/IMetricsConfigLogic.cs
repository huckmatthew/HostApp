using HostApp.Core.Interfaces;
using Owin;

namespace HostApp.Interfaces
{
    public interface IMetricsConfigLogic
    {
        void Config(IAppBuilder appBuilder, IApplicationConfiguration appConfig);
        //void Config(IAppBuilder appBuilder, IApplicationConfiguration appConfig);
        //MetricsSettings GetIndividualMetric(IEnumerable<EPT.Enterprise.DTO.Configuration.MetricsSettings> metric, string metricName);
    }
}
