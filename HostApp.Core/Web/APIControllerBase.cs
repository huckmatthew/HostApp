using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http;

namespace HostApp.Core.Web
{
    public class APIControllerBase : ApiController
    {
        [HttpGet]
        [Route("ping")]
        public IHttpActionResult GetPing()
        {
            var mybuffer = new List<string> { "Pinged", DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) };

            return Ok(mybuffer);
        }

    }
}
