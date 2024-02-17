using SampleUserManagement.Base.Entities;

namespace SampleUserManagement.Base.EntityFramework
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Add new entity to the repository
        /// </summary>
        /// <param name="entity"></param>
        void Create(TEntity entity);

        /// <summary>
        /// Update an entity data from repository
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete entity data from repository
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Get entity data by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity?> Get(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Get initial queryable function to create filter
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Filter();

        /// <summary>
        /// Calculate total data from specified filter. Useful when creating feature for pagination
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(IQueryable<TEntity> query, CancellationToken cancellationToken);

        /// <summary>
        /// Execute query filter
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
		Task<List<TEntity>> ExecuteAsync(IQueryable<TEntity> query, CancellationToken cancellationToken);
    }
}
