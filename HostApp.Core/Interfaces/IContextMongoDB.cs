using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HostApp.Core.Interfaces
{
    public interface IContextMongoDB<T>
    {
        IMongoClient Client { get; }
        IMongoCollection<BsonDocument> GetBsonCollection { get; }
        IMongoCollection<T> GetCollection { get; }
    }
}
