using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostApp.Core.DTO;
using MongoDB.Bson;

namespace HostApp.Core.Interfaces
{
    public interface IRepositoryMongoDB<TEntity> : IRepositoryMongoDBBase<TEntity>
        where TEntity : EntityMongoBase
    {
        ///// <summary>
        ///// Inserts an entity into the repository and sets the entity id
        ///// </summary>
        ///// <param name="entity">Entity to insert</param>
        ///// <returns>True if the insert has been successful otherwise false</returns>
        //bool Insert(TEntity entity);

        /// <summary>
        /// Saves (updates) an entity that is already in the repository
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>True if the update was successful otherwise false</returns>
        bool Update(TEntity entity);

        /// <summary>
        /// Removes an entity from the repository
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        /// <returns>True if an entity was deleted otherwise false</returns>
        bool Delete(TEntity entity);

        ///// <summary>
        ///// Searches for a list of entities that match a specified predicate
        ///// </summary>
        ///// <param name="predicate">Predicate to use when searching for entities</param>
        ///// <returns></returns>
        //IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        ///// <summary>
        ///// Retrieves all the entities from the repository
        ///// </summary>
        ///// <returns>List of entities</returns>
        //IEnumerable<TEntity> GetAll();
        ///// <summary>
        ///// Retrieves all the entities from the repository
        ///// </summary>
        ///// <returns>List of entities as a json string</returns>
        //string GetBsonAll();

        /// <summary>
        /// Retrieves an entity by its integer id
        /// </summary>
        /// <param name="id">Id of the entity to retrieve</param>
        /// <returns>A matching entity with the specified id</returns>
        TEntity GetById(ObjectId id);

    }
}
