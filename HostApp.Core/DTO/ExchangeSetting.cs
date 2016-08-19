using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostApp.Core.DTO
{
    public class ExchangeSetting
    {
        public string exchangeName { get; set; }
        public string exchangeType { get; set; }
        public bool passive { get; set; } // = false;
        public bool durable { get; set; } // = true;
        public bool autoDelete { get; set; } //= false;
    }
}
