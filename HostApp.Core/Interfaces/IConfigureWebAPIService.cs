using Autofac;
using Owin;

namespace HostApp.Core.Interfaces
{
    public interface IConfigureWebAPIService
    {
        void Configuration(IAppBuilder appBuilder, IContainer container);
    }
}
