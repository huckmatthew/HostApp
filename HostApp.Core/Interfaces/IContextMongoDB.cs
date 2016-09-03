﻿using MongoDB.Bson;
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
