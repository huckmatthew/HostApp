using System.Threading.Tasks;
using HostApp.Interfaces;
using log4net;
using Microsoft.Owin;

namespace HostApp.Logic
{
    public class MiddleWareBasicLogger : OwinMiddleware, IMiddleWareLogger
    {
        private ILog _log;

        public MiddleWareBasicLogger(OwinMiddleware next)
            : base(next)
        {
            ConfigureLog();

        }

        private void ConfigureLog()
        {
            _log = LogManager.GetLogger("MiddleWare");
        }

        public override async Task Invoke(IOwinContext context)
        {
            bool log = true;
            if (context.Request.Path.HasValue)
            {
                var path = context.Request.Path.ToString().ToLower();
                if (!path.StartsWith("/api"))
                    log = false;
            }
            if (log)
            {
                if (!_log.Logger.Repository.Configured)
                {
                    ConfigureLog();
                }
                _log.DebugFormat("{0} {1} {2}",
                    context.Request.Scheme,
                    context.Request.Method,
                    context.Request.Path
                    );
            }

            await Next.Invoke(context);
            if (log)
            {
                _log.DebugFormat("{0} {1}", context.Response, context.Response.StatusCode);

            }
        }
    }
}
