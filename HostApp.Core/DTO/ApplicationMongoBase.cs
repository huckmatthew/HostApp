using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostApp.Core.DTO
{
    public class ApplicationMongoBase : EntityMongoBase
    {
        public string hostName { get; set; }
        public string applicationName { get; set; }

    }
}
