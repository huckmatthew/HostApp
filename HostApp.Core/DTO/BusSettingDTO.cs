using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostApp.Core.DTO
{
    public class BusSettingDTO : ApplicationMongoBase
    {
        public string rabbitmqHostName { get; set; }// = "localhost";
        public int port { get; set; }// = 5672;
        public string userName { get; set; }// = "guest";
        public string password { get; set; }// = "guest";
        public ushort requestedHeartbeatMilliseconds { get; set; }// = 4000;	// or whatever
        public string virtualHost { get; set; }// = "/";
        public int connectionTimeoutMilliseconds { get; set; }// = 10000;	// or whatever
        public int connectionRetryAttempts { get; set; }// = 10;
    }
}
