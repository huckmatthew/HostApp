using Autofac;

namespace HostApp.Core.Interfaces
{
    public interface IConfigureIOC
    {
        void Configure(ContainerBuilder builder, object appConfig);
    }
}
