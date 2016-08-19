using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostApp.Core.DTO;

namespace HostApp.Core.Interfaces
{
    public interface IBusinessServiceConfiguration : IApplicationConfiguration
    {
        string startingAssembly { get; set; }
        BusSettingDTO busConfiguration { get; set; }
        int maxRetryCount { get; set; }
        int retrySleepInSeconds { get; set; }
    }
}
