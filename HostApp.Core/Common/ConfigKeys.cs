using System.Runtime.Serialization;

namespace HostApp.Core.Common
{
    [DataContract]
    public enum ConfigKeys
    {
        [EnumMember]
        ApplicationConfigurationRespository,
        [EnumMember]
        ApplicationConfiguration,
        [EnumMember]
        DefaultApplication,
        [EnumMember]
        Metrics,
        [EnumMember]
        MongoDB

    }
}
