using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using HostApp.Core.Constants;
using HostApp.Core.DTO;
using HostApp.Core.Interfaces;
using HostApp.Core.Repository;
using HostApp.Core.Utility;

namespace HostApp.BootStrap
{
    public class LoadIOC
    {
        /// <summary>
        /// Finds all the methods that implement IConfigurIOC and call them. 
        /// </summary>
        /// <param name="appConfig"></param>
        /// <param name="apiService"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public IContainer Get(object appConfig, IService apiService, IContainer container)
        {
            var hostImps = AssemblyHelper.GetImplementations(typeof(IConfigureIOC));
            if (hostImps != null)
            {
                var hostIoc = hostImps.ToArray();

                var builder = new ContainerBuilder();

                var config = (IRestServiceConfiguration)appConfig;

                builder.RegisterInstance(apiService).As<IService>();
                builder.RegisterType<Startup>().As<IConfigureWebAPIService>();

                //Config
                builder.RegisterInstance(appConfig).As<IApplicationConfiguration>();
                builder.RegisterInstance(appConfig).As<IRestServiceConfiguration>();

                //Controllers
                var dllcontrollers = AssemblyHelper.GetImplementations(typeof(ApiController));
                var assembly = Assembly.GetExecutingAssembly();
                builder.RegisterApiControllers(assembly);
                foreach (var dllcontroller in dllcontrollers)
                {
                    builder.RegisterApiControllers(Assembly.GetAssembly(dllcontroller));

                }
                if (config.IsConfigured)
                {

                    ////Mongo Connections
                    builder.Register(
                        c => new ContextMongoDB<SubscriptionSettingDTO>(config.MongoDBConnection, MongoConstants.ConfigurationDatabase))
                        .As<IContextMongoDB<SubscriptionSettingDTO>>();

                    ////Repos
                    AutofacHelper.RegisterDLL(builder, "Host*.dll", "Repository");

                    ////Logic
                    AutofacHelper.RegisterDLL(builder, "Host*.exe", "Logic");
                    AutofacHelper.RegisterDLL(builder, "Host*.dll", "Logic");

                    if (hostIoc.Any())
                    {
                        foreach (var hostImp in hostIoc)
                        {
                            var theInstance = (IConfigureIOC)Activator.CreateInstance(hostImp);

                            theInstance?.Configure(builder, appConfig);
                        }
                    }

                }
                if (container == null)
                {
                    var newcontainer = builder.Build();
                    return newcontainer;
                }

                builder.Update(container);
                return container;
            }
            return null;
        }
    }
}
