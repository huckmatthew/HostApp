using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HostApp.Core.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HostApp.Core.Repository
{
    public class RespositoryMongoDBBase<TEntity> : IRepositoryMongoDBBase<TEntity>
    {
        protected readonly IContextMongoDB<TEntity> Context;

        public RespositoryMongoDBBase(IContextMongoDB<TEntity> context)
        {
            Context = context;

        }

        public bool Insert(TEntity entity)
        {
            var results = Context.GetCollection.InsertOneAsync(entity);
            return results.IsCompleted;
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            Task<List<TEntity>> data = Context.GetCollection.Find(predicate).ToListAsync();

            return await data;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            Task<List<TEntity>> data = Context.GetCollection.FindAsync(new BsonDocument()).Result.ToListAsync();

            return await data;
            //var returndata = await data;
            //return returndata.ToArray();
        }

        public string GetBsonAll()
        {

            var data = Context.GetBsonCollection.FindAsync(new BsonDocument()).Result.ToListAsync();

            string data2 = data.ToJson();

            return data2;
        }

    }
}
