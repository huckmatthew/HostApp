using System.Threading.Tasks;
using HostApp.Core.Interfaces;

namespace HostApp.Interfaces
{
    public interface IApplicationRepository
    {
        IBusinessServiceConfiguration GetBusinessServiceAppConfig(string hostName, string applicationName);
        IServiceHostConfiguration GetServiceHostConfig(string hostName, string applicationName);
        //Task<IIdentityApiConfiguration> GetIdentityApiAppConfig(string hostName, string applicationName);
        //Task<IIdentityWebConfiguration> GetIdentityWebAppConfig(string hostName, string applicationName);
        IRestServiceConfiguration GetRestServiceAppConfig(string hostName, string applicationName);
    }
}
