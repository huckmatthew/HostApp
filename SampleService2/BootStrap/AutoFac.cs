using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using HostApp.Core.Interfaces;
using log4net;
using log4net.Core;

namespace SampleService2.BootStrap
{
    public class AutoFac : IConfigureIOC
    {
        private ILog _log;

        public AutoFac()
        {
            _log = LogManager.GetLogger("$safeprojectname$");
            _log.Debug("AutoFac Created!");

        }

        public void Configure(ContainerBuilder builder, object appConfig)
        {

            _log.Debug("Starting Configure IOC Ping");
            var config = (IRestServiceConfiguration)appConfig;

            builder.Register(c => LogManager.GetLogger("$safeprojectname$")).As<ILog>();

            _log.Debug("Finished Configure IOC $safeprojectname$");
        }

    }
}
