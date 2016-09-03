using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HostApp.Core.DTO;
using HostApp.Core.Interfaces;
using HostApp.Core.Repository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HostApp.Repository.Mongo
{
    public class RespositoryMongoDB<TEntity> : RespositoryMongoDBBase<TEntity>, IRepositoryMongoDB<TEntity>
        where TEntity : EntityMongoBase
    {

        public RespositoryMongoDB(IContextMongoDB<TEntity> context) : base(context)
        {
        }


        public bool Update(TEntity entity)
        {
            Task<ReplaceOneResult> results = Context.GetCollection.ReplaceOneAsync(d => d.Id.Equals(entity.Id), entity,
                new UpdateOptions { IsUpsert = false });
            return results.Result.ModifiedCount == 1;
        }

        public bool Delete(TEntity entity)
        {
            Task<DeleteResult> result = Context.GetCollection.DeleteOneAsync(x => x.Id == entity.Id);
            if (result.Result.DeletedCount == 1) return true;

            return false;
        }

        public TEntity GetById(ObjectId id)
        {
            List<TEntity> data = Context.GetCollection.FindAsync(x => x.Id == id).Result.ToListAsync().Result;

            return data.Count == 1 ? data.First() : null;
        }

    }
}
