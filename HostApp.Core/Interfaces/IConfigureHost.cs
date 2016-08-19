using System.Threading.Tasks;
using Autofac;

namespace HostApp.Core.Interfaces
{
    public interface IConfigureHost
    {
        Task<object> LoadConfiguration(string hostName, string appToLoad);

        void ConfigureCaching(ContainerBuilder builder, IApplicationConfiguration config);

    }
}
