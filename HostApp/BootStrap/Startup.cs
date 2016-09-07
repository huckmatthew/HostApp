using System;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using HostApp.Core.Interfaces;
using HostApp.Logic;
using log4net;
using Owin;
using IMetricsConfigLogic = HostApp.Interfaces.IMetricsConfigLogic;

namespace HostApp.BootStrap
{
    public class Startup : IConfigureWebAPIService
    {
        /// <exception cref="Exception">Application config  not configured correctly</exception>
        public void Configuration(IAppBuilder appBuilder, IContainer container)
        {
            var log = LogManager.GetLogger("Startup");

            log.Debug("Starting WebAPI Startup");

            // Configure Web API for self-host. 
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            var appConfig = container.Resolve<IApplicationConfiguration>();
            if (appConfig == null)
                throw new Exception("Application not configured correctly");

            if (container.IsRegistered<IMetricsConfigLogic>())
            {
                var metricsConfig = container.Resolve<IMetricsConfigLogic>();
                metricsConfig.Config(appBuilder, appConfig);
            }

            appBuilder.Use(typeof(MiddleWareBasicLogger));

            appBuilder.UseAutofacMiddleware(container);

            appBuilder.UseAutofacWebApi(config);

            appBuilder.UseWebApi(config);

        }

    }
}
