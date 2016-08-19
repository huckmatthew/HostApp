using HostApp.Core.Extensions;
using HostApp.Core.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace HostApp.Core.Repository
{
    public class ContextMongoDB<T> : IContextMongoDB<T>
    {

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly string _collectionName;

        /// <summary>
        /// Database context for MongoDB
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databasename"></param>
        public ContextMongoDB(string connectionString, string databasename)
        {
            var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databasename);
            _collectionName = GetCollectionName(typeof(T).Name);

        }

        public IMongoClient Client
        {
            get { return _client; }
        }

        public IMongoCollection<BsonDocument> GetBsonCollection
        {
            get { return _database.GetCollection<BsonDocument>(_collectionName); }
        }

        public IMongoCollection<T> GetCollection
        {
            get { return _database.GetCollection<T>(_collectionName); }
        }

        private string GetCollectionName(string collectionName)
        {
            var workingName = collectionName.ToLower();

            if (workingName.EndsWith("dto"))
            {
                workingName = workingName.Left(workingName.Length - 3);
            }
            return workingName;
        }
    }
}
