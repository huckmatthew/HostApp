using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HostApp.Core.DTO;
using HostApp.Core.Interfaces;
using MongoDB.Driver;

namespace HostApp.Repository.Mongo
{
    public class BusSettingsMongoDBRepository : RespositoryMongoDB<BusSettingDTO>, IBusConfigurationRepository
    {
        public BusSettingsMongoDBRepository(IContextMongoDB<BusSettingDTO> context) : base(context)
        {
        }

        public BusSettingDTO Get(string hostName, string applicationName)
        {
            Expression<Func<BusSettingDTO, bool>> filter = x => x.hostName.Equals(hostName) && x.applicationName.Equals(applicationName);

            var data = Context.GetCollection.Find(filter).FirstOrDefault();

            if (data == null)
            {
                filter = x => x.hostName.Equals("default") && x.applicationName.Equals(applicationName);
                data = Context.GetCollection.Find(filter).FirstOrDefault();

                if (data == null)
                {
                    filter = x => x.hostName.Equals("default") && x.applicationName.Equals("default");
                    data = Context.GetCollection.Find(filter).FirstOrDefault();
                    //if Null that is ok not bus for this app
                    return data;
                }
            }

            return null;
        }

    }
}
