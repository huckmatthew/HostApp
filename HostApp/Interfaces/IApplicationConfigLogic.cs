using System.Threading.Tasks;
using HostApp.Core.Interfaces;

namespace HostApp.Interfaces
{
    public interface IApplicationConfigLogic
    {
        IRestServiceConfiguration GetRestServiceAppConfig(string connectionString, string hostName, string applicationName);

    }
}
