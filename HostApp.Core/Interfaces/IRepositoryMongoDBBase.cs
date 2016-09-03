using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HostApp.Core.Interfaces
{
    public interface IRepositoryMongoDBBase<TEntity>
    {
        /// <summary>
        /// Inserts an entity into the repository and sets the entity id
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <returns>True if the insert has been successful otherwise false</returns>
        bool Insert(TEntity entity);

        /// <summary>
        /// Searches for a list of entities that match a specified predicate
        /// </summary>
        /// <param name="predicate">Predicate to use when searching for entities</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Retrieves all the entities from the repository
        /// </summary>
        /// <returns>List of entities</returns>
        Task<IEnumerable<TEntity>> GetAll();
        /// <summary>
        /// Retrieves all the entities from the repository
        /// </summary>
        /// <returns>List of entities as a json string</returns>
        string GetBsonAll();

    }
}
