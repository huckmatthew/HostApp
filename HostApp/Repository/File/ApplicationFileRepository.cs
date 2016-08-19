using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostApp.Core.Common;
using HostApp.Core.DTO;
using HostApp.Core.Interfaces;
using HostApp.Interfaces;
using Newtonsoft.Json;

namespace HostApp.Repository.File
{
    public class ApplicationFileRepository : IApplicationRepository
    {
        public IBusinessServiceConfiguration GetBusinessServiceAppConfig(string hostName, string applicationName)
        {
            return GetConfiguration<BusinessServiceConfiguration>(ConfigKeys.ApplicationConfiguration);
        }


        public IServiceHostConfiguration GetServiceHostConfig(string hostName, string applicationName)
        {
            return GetConfiguration<ServiceHostConfiguration>(ConfigKeys.ApplicationConfiguration);
        }


        public IRestServiceConfiguration GetRestServiceAppConfig(string hostName, string applicationName)
        {
            return GetConfiguration<RestServiceConfiguration>(ConfigKeys.ApplicationConfiguration);
        }

        private T GetConfiguration<T>(ConfigKeys key)
        {
            IConfigWrapper configWrapper = new ConfigWrapper();
            var setingsjson = configWrapper.GetTemplate(key);

            return  JsonConvert.DeserializeObject<T>(setingsjson);
        }

    }
}
