using HostApp.Core.Common;

namespace HostApp.Core.Interfaces
{
    public interface IConfigWrapper
    {
        string GetPath(ConfigKeys key);
        string GetTemplate(ConfigKeys key);
        string GetValue(ConfigKeys key);
        string GetConnectionValue(ConfigKeys key);

    }
}
