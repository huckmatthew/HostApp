using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostApp.Core.DTO
{
    public class SubscriptionSettingDTO : EntityMongoBase
    {
        public string applicationName { get; set; }
        public string routingKey { get; set; }// = "";
        public ushort prefetchCount { get; set; }// = 50;
        public QueueSetting queueConfiguration { get; set; }
        public ExchangeSetting exchangeConfiguration { get; set; }

    }
}
