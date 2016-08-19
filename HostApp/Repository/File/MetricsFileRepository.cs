using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HostApp.Core.Common;
using HostApp.Core.DTO;
using HostApp.Core.Interfaces;
using Newtonsoft.Json;

namespace HostApp.Repository.File
{
    public class MetricsFileRepository : IMetricsRepository
    {
        public IEnumerable<MetricsSettingDTO> GetAll(string hostName, string applicationName)
        {
            IConfigWrapper configWrapper = new ConfigWrapper();
            List<MetricsSettingDTO> settings = new List<MetricsSettingDTO>();
            var setingsjson = configWrapper.GetTemplate(ConfigKeys.Metrics);

            using (var reader = new StringReader(setingsjson))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var temp = JsonConvert.DeserializeObject<MetricsSettingDTO>(line);
                    settings.Add(temp);
                }
            }

            return settings;

        }
    }
}
