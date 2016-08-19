using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace HostApp.Core.DTO
{
    public class EntityMongoBase
    {
        [BsonId]
        public virtual ObjectId Id { get; set; }
    }
}
