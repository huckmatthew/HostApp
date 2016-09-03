using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using HostApp.BootStrap;
using HostApp.Core.Extensions;
using HostApp.Core.Interfaces;
using Topshelf;

namespace HostApp
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            var serviceRunner = HostFactory.New(
            x =>
            {
                var appconfig = LoadAppConfig();

                IService webapi = new WebAPIService();

                var container = LoadIOCContainer(appconfig, webapi);

                x.UseLog4Net();
                x.UseLinuxIfAvailable();
                x.Service<IService>(sc =>
                {
                    sc.ConstructUsing(svc => webapi);
                    sc.WhenStarted((s, hostControl) => s.Start(hostControl, container));
                    sc.WhenStopped(s => s.Stop());
                });

                x.RunAsLocalService();

                x.SetDescription("{0}".FormatWith(((IApplicationConfiguration)appconfig).ApplicationName));
                x.SetDisplayName("{0}".FormatWith(((IApplicationConfiguration)appconfig).ApplicationName));
                x.SetServiceName("HostApp{0}".FormatWith(((IApplicationConfiguration)appconfig).ApplicationName));
                x.EnableShutdown();
            });

            try
            {
                serviceRunner.Run();
            }
            catch (Exception ex)
            {
                var appLog = new EventLog { Source = Assembly.GetCallingAssembly().GetName().Name };
                var message = string.Format("ERROR connect to or getting configuration data. {0}\n{1}\n{2}".FormatWith( ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty, "Main"));
                appLog.WriteEntry(message, EventLogEntryType.Error);
                Console.WriteLine(message);
                Thread.Sleep(10 * 1000);
            }
        }

        private static object LoadAppConfig()
        {
            var loadConfig = new LoadConfiguration();

            return loadConfig.Get();
        }

        private static IContainer LoadIOCContainer(object appConfig, IService apiService)
        {
            var loadIOC = new LoadIOC();
            return loadIOC.Get(appConfig, apiService, null);
        }

    }
}

