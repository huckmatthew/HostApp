using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HostApp.Core.Common;
using HostApp.Core.DTO;
using HostApp.Core.Interfaces;
using MongoDB.Driver;

namespace HostApp.Repository.Mongo
{
    public class MetricsMongDBRepository : RespositoryMongoDB<MetricsSettingDTO>, IMetricsRepository
    {

        public MetricsMongDBRepository(IContextMongoDB<MetricsSettingDTO> context) : base(context)
        {
        }

        public IEnumerable<MetricsSettingDTO> GetAll(string hostName, string applicationName)
        {

            Expression<Func<MetricsSettingDTO, bool>> filter = x => x.hostName.Equals(hostName) && x.applicationName.Equals(applicationName);

            var data = Context.GetCollection.Find(filter).ToList();

            if (!data.Any())
            {
                filter = x => x.hostName.Equals("default") && x.applicationName.Equals(applicationName);
                data = Context.GetCollection.Find(filter).ToList();

                if (!data.Any())
                {
                    filter = x => x.hostName.Equals("default") && x.applicationName.Equals("default");
                    data = Context.GetCollection.Find(filter).ToList();
                    if (!data.Any())
                    {
                        throw new ApplicationException(ApplicationError.MetricsSettingsNotDefined);
                    }
                }
            }

            return data;
        }
    }
}
