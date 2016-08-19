using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using HostApp.Core.Interfaces;
using log4net;
using log4net.Core;

namespace SampleService1.BootStrap
{
    public class AutoFac : IConfigureIOC
    {
        private ILog _log;

        public AutoFac()
        {
            _log = LogManager.GetLogger("SampleService1");
            _log.Debug("AutoFac Created!");

        }

        public void Configure(ContainerBuilder builder, object appConfig)
        {
            //var log = EPTLogManager.GetLogger(this.GetType());

            _log.Debug("Starting Configure IOC Ping");
            var config = (IRestServiceConfiguration)appConfig;

            //Services
            //builder.RegisterType<SettingsLogic>().As<ISettingsLogic>();
            //builder.RegisterType<LocalMetricsConfig>().As<IMetricsConfigLogic>();
            //SQL Repository
            //builder.RegisterType<SettingsSQLRepository>().As<ISettingsRepository>();
            //MongoDB Repository
            //builder.Register(c=> new SettingsMongoRepository( config.MongoDBConnection, MongoConstants.ConfigurationDatabase)).As<ISettingsRepository>();

            builder.Register(c => LogManager.GetLogger("SampleService1")).As<ILog>();

            _log.Debug("Finished Configure IOC SampleService1");
        }

    }
}
