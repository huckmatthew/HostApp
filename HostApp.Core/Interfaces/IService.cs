using Autofac;
using Topshelf;

namespace HostApp.Core.Interfaces
{
    public interface IService
    {
        bool Start(HostControl hostControl, IContainer container);
        bool Stop();
        bool Reload(string applicationName);
    }
}
