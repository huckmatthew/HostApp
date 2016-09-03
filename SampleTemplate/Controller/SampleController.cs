using System;
using System.Web.Http;
using HostApp.Core.Extensions;
using HostApp.Core.Web;
using log4net;
using Metrics;

namespace SampleTemplate.Controller
{
    [RoutePrefix("api/sample")]
    public class SampleController : APIControllerBase
    {
        private readonly Counter _counter = Metric.Counter("Count", Unit.Requests);
        private readonly Timer _timer = Metric.Timer("Requests", Unit.Requests);
        private ILog _log;

        public SampleController(ILog log)
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

            using (_timer.NewContext())
            {
                _log.Debug("Processing -{0}".FormatWith(name));


                var temp = "Hello {0}.".FormatWith(name);
                return Ok(temp);
            }
        }

    }
}
