using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostApp.Core.DTO
{
    public class QueueSetting
    {
        public string queueName { get; set; }
        public bool passive { get; set; }// = false;
        public bool durable { get; set; }// = true;
        public bool autoDelete { get; set; }// = false;
        public bool exclusive { get; set; }// = false;
        public int? messageTtl { get; set; }// = null;
        public int? expires { get; set; } //= null;
        public string deadLetterExchange { get; set; }// = "";
        public string deadLetterRoutingKey { get; set; }// = "";
    }
}
