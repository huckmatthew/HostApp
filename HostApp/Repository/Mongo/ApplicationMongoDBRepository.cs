using System;
using System.Threading.Tasks;
using HostApp.Core.Common;
using HostApp.Core.DTO;
using HostApp.Core.Extensions;
using HostApp.Core.Interfaces;
using HostApp.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace HostApp.Repository.Mongo
{
    public class ApplicationMongoDBRepository : IApplicationRepository
    {
        private readonly IMongoDatabase _database;
        private const string ApplicationsCollection = "application";
        //private const string FindbyApplicationName = "{{ applicationName : \"{0}\" }}";
        //private const string FindbyHostandApplicationName = "{{hostName : \"{0}\", applicationName : \"{1}\" }}";

        private const string FindbyApplicationName = " {{hostName : \"default\", applicationName : \"{0}\" }}";
        //private const string FindbyDefault = "{ hostName : \"default\", applicationName : \"default\" }";
        private const string FindbyHostandApplicationName = "{{hostName : \"{0}\", applicationName : \"{1}\" }}";

        public ApplicationMongoDBRepository(string connectionString, string databasename)
        {
            var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

            IMongoClient client = new MongoClient(connectionString);
            _database = client.GetDatabase(databasename);
        }

        public IBusinessServiceConfiguration GetBusinessServiceAppConfig(string hostName, string applicationName)
        {
            return GetConfiguration<BusinessServiceConfiguration>(hostName, applicationName);
        }

        //public async Task<IOrchestrationConfiguration> GetOrchestrationAppConfig(string hostName, string applicationName)
        //{
        //    return await GetConfiguration<OrchestrationConfiguration>(hostName, applicationName);
        //}

        public IServiceHostConfiguration GetServiceHostConfig(string hostName, string applicationName)
        {
            return GetConfiguration<ServiceHostConfiguration>(hostName, applicationName);
        }

        //public async Task<IIdentityApiConfiguration> GetIdentityApiAppConfig(string hostName, string applicationName)
        //{
        //    return await GetConfiguration<IdentityApiConfiguration>(hostName, applicationName);
        //}

        //public async Task<IIdentityWebConfiguration> GetIdentityWebAppConfig(string hostName, string applicationName)
        //{
        //    return await GetConfiguration<IdentityWebConfiguration>(hostName, applicationName);
        //}

        public IRestServiceConfiguration GetRestServiceAppConfig(string hostName, string applicationName)
        {
            return GetConfiguration<RestServiceConfiguration>(hostName, applicationName);
        }

        private T GetConfiguration<T>(string hostName, string applicationName)
        {
            var filter = FindbyHostandApplicationName.FormatWith(hostName, applicationName);
            var data = _database.GetCollection<BsonDocument>(ApplicationsCollection).Find(filter).FirstOrDefault();

            if (data == null)
            {
                filter = string.Format(FindbyApplicationName, applicationName);
                data = _database.GetCollection<BsonDocument>(ApplicationsCollection).Find(filter).FirstOrDefault();

                if (data == null)
                {
                    throw new ApplicationException(ApplicationError.AppicationNotDefined);
                }
            }

            return BsonSerializer.Deserialize<T>(data);
        }
    }
}
