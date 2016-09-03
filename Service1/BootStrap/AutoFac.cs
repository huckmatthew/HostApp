using Autofac;
using HostApp.Core.Interfaces;
using log4net;

namespace Service1.BootStrap
{
    public class AutoFac : IConfigureIOC
    {
        private ILog _log;

        public AutoFac()
        {
            _log = LogManager.GetLogger("Service1");
            _log.Debug("AutoFac Created!");

        }

        public void Configure(ContainerBuilder builder, object appConfig)
        {

            _log.Debug("Starting Configure IOC Ping");
            var config = (IRestServiceConfiguration)appConfig;

            builder.Register(c => LogManager.GetLogger("Service1")).As<ILog>();

            _log.Debug("Finished Configure IOC Service1");
        }

    }
}
