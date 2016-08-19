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
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApiAction",
            //    routeTemplate: "api/{controller}/{action}/");

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional });


            var appConfig = container.Resolve<IApplicationConfiguration>();
            if (appConfig == null)
                throw new Exception("Application config  not configured correctly");

            if (container.IsRegistered<IMetricsConfigLogic>())
            {
                var metricsConfig = container.Resolve<IMetricsConfigLogic>();
                metricsConfig.Config(appBuilder, appConfig);
            }

            appBuilder.Use(typeof(MiddleWareBasicLogger));

            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            appBuilder.UseAutofacMiddleware(container);

            appBuilder.UseAutofacWebApi(config);

            appBuilder.UseWebApi(config);

        }

    }
}
