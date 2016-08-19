using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostApp.Core.Interfaces;

namespace HostApp.Core.DTO
{
    public class ApplicationConfiguration : EntityMongoBase, IApplicationConfiguration
    {
        public string MongoDBConnection { get; set; }
        public string HostName { get; set; }
        public string ApplicationName { get; set; }
        public bool IsConfigured { get; set; }
        public int servicePort { get; set; }
        public string url { get; set; }
        public bool logMetricstoMongo { get; set; }
        //public IEnumerable<SQLConnectionDTO> SQLConnection { get; set; }
        public IEnumerable<MetricsSettingDTO> MetricConfiguration { get; set; }
        //public LogSettingDTO LogConfiguration { get; set; }
        public ExchangeSetting MessageExchange { get; set; }
        //public IEnumerable<CacheConfigurationDTO> cacheConfiguration { get; set; }
    }
}