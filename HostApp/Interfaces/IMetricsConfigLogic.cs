using HostApp.Core.Interfaces;
using Owin;

namespace HostApp.Interfaces
{
    public interface IMetricsConfigLogic
    {
        void Config(IAppBuilder appBuilder, IApplicationConfiguration appConfig);
    }
}
