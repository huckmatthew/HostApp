using System.Collections.Generic;
using HostApp.Core.DTO;

namespace HostApp.Core.Interfaces
{
    public interface IMetricsRepository
    {
        IEnumerable<MetricsSettingDTO> GetAll(string hostName, string applicationName);
    }
}
