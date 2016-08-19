using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostApp.Core.Interfaces;

namespace HostApp.Core.DTO
{
    public class BusinessServiceConfiguration : ApplicationConfiguration, IBusinessServiceConfiguration
    {
        public string startingAssembly { get; set; }
        public BusSettingDTO busConfiguration { get; set; }
        public int maxRetryCount { get; set; }
        public int retrySleepInSeconds { get; set; }
    }
}
