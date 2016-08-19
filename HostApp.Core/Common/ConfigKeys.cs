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
        //[EnumMember]
        //Bus,
        //[EnumMember]
        //CacheConfiguration,
        [EnumMember]
        DefaultApplication,
        //[EnumMember]
        //LogSettings,
        [EnumMember]
        Metrics,
        [EnumMember]
        MongoDB,
        //[EnumMember]
        //SQLConnections,
        //[EnumMember]
        //SubscriptionSettings

    }
}
