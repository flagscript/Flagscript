using System.Threading.Tasks;

using Flagscript.Data.Entity;

namespace Flagscript.Data.Repository
{

	/// <summary>
	/// Interface defining an entity repository with read and write operations.
	/// </summary>
	public interface IRepository : IReadOnlyRepository
	{

		/// <summary>
		/// Marks an entity to be created.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <param name="createdBy">The principal who is creating the entity.</param>
		/// <typeparam name="TEntity">The type of the <see cref="IEntity"/> to create.</typeparam>
		void Create<TEntity>(TEntity entity, string createdBy = null)
			where TEntity : class, IEntity;

		/// <summary>
		/// Marks an entity to be updated.
		/// </summary>
		/// <param name="entity">The entity to update with its updated values.</param>
		/// <param name="modifiedBy">The principal who modified the entity.</param>
		/// <typeparam name="TEntity">The type of the <see cref="IEntity"/> to update.</typeparam>
		void Update<TEntity>(TEntity entity, string modifiedBy = null)
			where TEntity : class, IEntity;

		/// <summary>
		/// Marks an entity for deletion by its identifier.
		/// </summary>
		/// <param name="id">The identifier of the entity to delete.</param>
		/// <typeparam name="TEntity">The type of the <see cref="IEntity"/> to delete.</typeparam>
		void Delete<TEntity>(object id)
			where TEntity : class, IEntity;

		/// <summary>
		/// Marks an entity for deletion.
		/// </summary>
		/// <param name="entity">The entity to mark for deletion.</param>
		/// <typeparam name="TEntity">The type of the <see cref="IEntity"/> to delete.</typeparam>
		void Delete<TEntity>(TEntity entity) 
			where TEntity : class, IEntity;

		/// <summary>
		/// Saves all entities marked for creation, update or deletion to the data store.
		/// </summary>
		/// <returns>The number of all entries written to the data store.</returns>
		int Save();

		/// <summary>
		/// Asynchronously saves all entities marked for creation, update or deletion to the data store.
		/// </summary>
		/// <returns>The number of all entries written to the data store.</returns>
		Task<int> SaveAsync();

	}

}
