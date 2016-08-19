using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostApp.Core.DTO
{
    public class MetricsSettingDTO : ApplicationMongoBase
    {

        public string name { get; set; }

        public string url { get; set; }

        public bool enabled { get; set; }
    }
}
