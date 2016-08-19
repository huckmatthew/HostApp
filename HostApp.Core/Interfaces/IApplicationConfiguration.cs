using System.Collections.Generic;
using HostApp.Core.DTO;

namespace HostApp.Core.Interfaces
{
    public interface IApplicationConfiguration
    {
        string HostName { get; set; }
        string ApplicationName { get; set; }
        bool IsConfigured { get; set; }
        int servicePort { get; set; }
        string url { get; set; }
        string MongoDBConnection { get; set; }
        IEnumerable<MetricsSettingDTO> MetricConfiguration { get; set; }
    }
}
