using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostApp.Core.Common;
using HostApp.Core.DTO;
using HostApp.Core.Interfaces;
using Newtonsoft.Json;

namespace HostApp.Repository.File
{
    public class BusSettingsFileRepository : IBusConfigurationRepository
    {
        public BusSettingDTO Get(string hostName, string applicationName)
        {
            IConfigWrapper configWrapper = new ConfigWrapper();
            var settings = new BusSettingDTO();
            var setingsjson = configWrapper.GetTemplate(ConfigKeys.Metrics);

            using (var reader = new StringReader(setingsjson))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var temp = JsonConvert.DeserializeObject<BusSettingDTO>(line);
                    settings = temp;
                }
            }

            return settings;
        }
    }
}
