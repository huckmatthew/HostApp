using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using HostApp.Core.Extensions;
using log4net;
using Metrics;

namespace SampleService1.Controller
{
    [RoutePrefix("api/sample1")]
    public class Sample1Controller : ApiController
    {
        private readonly Counter _counter = Metric.Counter("Count", Unit.Requests);
        private readonly Timer _meter = Metric.Timer("Errors", Unit.Requests);
        private ILog _log;

        public Sample1Controller(ILog log)
        {
            _log = log;
        }

        [HttpGet]
        [Route("HelloWorld/{Name}")]
        public IHttpActionResult HelloWorld(string name)
        {
            _counter.Increment();

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            using (_meter.NewContext())
            {
                _log.Debug("Processing -{0}".FormatWith(name));


                var temp = "Hello {0}.".FormatWith(name);
                return Ok(temp);
            }
        }


    }
}
