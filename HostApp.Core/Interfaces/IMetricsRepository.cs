using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostApp.Core.DTO;

namespace HostApp.Core.Interfaces
{
    public interface IMetricsRepository
    {
        IEnumerable<MetricsSettingDTO> GetAll(string hostName, string applicationName);
    }
}
