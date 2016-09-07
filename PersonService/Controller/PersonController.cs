using System;
using System.Web.Http;
using HostApp.Core.Extensions;
using HostApp.Core.Web;
using log4net;
using Metrics;
using PersonService.Interfaces;

namespace PersonService.Controller
{
    [RoutePrefix("api/person")]
    public class PersonController : APIControllerBase
    {
        private readonly Counter _counter = Metric.Counter("Total Requests", Unit.Requests);
        private readonly Counter _counter2 = Metric.Counter("Total Requests2", Unit.Requests);
        private readonly Timer _timerGet = Metric.Timer("GetPerson", Unit.Requests);
        private readonly Timer _timerSearch = Metric.Timer("SearchPerson", Unit.Requests);
        private ILog _log;
        private readonly IPersonLogic _personLogic;

        public PersonController(ILog log, IPersonLogic personLogic)
        {
            _log = log;
            _personLogic = personLogic;
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int? id)
        {
            _counter.Increment();
            _counter2.Increment();
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            using (_timerGet.NewContext(id.ToString()))
            {
                _log.Debug("Processing -{0}".FormatWith(id));

                var person = _personLogic.Get((int)id);
                return Ok(person);
            }
        }

        [HttpGet]
        [Route("Search/{lastname}")]
        public IHttpActionResult Search(string lastName)
        {
            _counter.Increment();

            if (lastName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            using (_timerSearch.NewContext(lastName))
            {
                _log.Debug("Processing -{0}".FormatWith(lastName));

                var person = _personLogic.Search(lastName);
                return Ok(person);
            }
        }

    }
}
