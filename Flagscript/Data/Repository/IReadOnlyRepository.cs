using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Flagscript.Data.Entity;

namespace Flagscript.Data.Repository
{

	/// <summary>
	/// Interface defining an entity repository for read-only operations.
	/// </summary>
	public interface IReadOnlyRepository
	{

		/// <summary>
		/// Gets all <see cref="IEntity"/> of a given type.
		/// </summary>
		/// <param name="orderBy">Function to order the entities by.</param>
		/// <param name="includeProperties">Comma separated string of relationship
		/// properties to include.</param>
		/// <param name="skip">Optional number of records to skip.</param>
		/// <param name="take">Optional number of records to take.</param>
		/// <returns>All <see cref="IEntity"/> of a given type.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		IEnumerable<TEntity> GetAll<TEntity>(
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null,
			int? skip = null,
			int? take = null)
			where TEntity : class, IEntity;

		/// <summary>
		/// Asynchronously gets all <see cref="IEntity"/> of a given type.
		/// </summary>
		/// <param name="orderBy">Function to order the entities by.</param>
		/// <param name="includeProperties">Comma separated string of relationship
		/// properties to include.</param>
		/// <param name="skip">Optional number of records to skip.</param>
		/// <param name="take">Optional number of records to take.</param>
		/// <returns>All <see cref="IEntity"/> of a given type.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null,
			int? skip = null,
			int? take = null)
			where TEntity : class, IEntity;

		/// <summary>
		/// Gets <see cref="IEntity"/> of a given type matching a filter.
		/// </summary>
		/// <param name="filter">The expression used to filter the entities.</param>
		/// <param name="orderBy">Function to order the entities by.</param>
		/// <param name="includeProperties">Comma separated string of relationship
		/// properties to include.</param>
		/// <param name="skip">Optional number of records to skip.</param>
		/// <param name="take">Optional number of records to take.</param>
		/// <returns>All entities matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		IEnumerable<TEntity> Get<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null,
			int? skip = null,
			int? take = null)
			where TEntity : class, IEntity;

		/// <summary>
		/// Asynchronously gets <see cref="IEntity"/> of a given type matching a filter.
		/// </summary>
		/// <param name="filter">The expression used to filter the entities.</param>
		/// <param name="orderBy">Function to order the entities by.</param>
		/// <param name="includeProperties">Comma separated string of relationship
		/// properties to include.</param>
		/// <param name="skip">Optional number of records to skip.</param>
		/// <param name="take">Optional number of records to take.</param>
		/// <returns>All entities matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		Task<IEnumerable<TEntity>> GetAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null,
			int? skip = null,
			int? take = null)
			where TEntity : class, IEntity;

		/// <summary>
		/// Gets the single <see cref="IEntity"/> of a given type matching a filter.
		/// </summary>
		/// <param name="filter">The expression used to filter the entity.</param>
		/// <param name="includeProperties">Comma separated string of relationship
		/// properties to include.</param>
		/// <returns>The single entity matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		TEntity GetOne<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			string includeProperties = null)
			where TEntity : class, IEntity;

		/// <summary>
		/// Asynchronously gets the single <see cref="IEntity"/> of a given type matching a filter.
		/// </summary>
		/// <param name="filter">The expression used to filter the entity.</param>
		/// <param name="includeProperties">Comma separated string of relationship
		/// properties to include.</param>
		/// <returns>The single entity matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		Task<TEntity> GetOneAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			string includeProperties = null)
			where TEntity : class, IEntity;

		/// <summary>
		/// Gets the first <see cref="IEntity"/> of a given type matching a filter.
		/// </summary>
		/// <param name="filter">The expression used to filter the entity.</param>
		/// <param name="includeProperties">Comma separated string of relationship
		/// properties to include.</param>
		/// <returns>The first entity matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		TEntity GetFirst<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null)
			where TEntity : class, IEntity;

		/// <summary>
		/// Asynchronously gets the first <see cref="IEntity"/> of a given type matching a filter.
		/// </summary>
		/// <param name="filter">The expression used to filter the entity.</param>
		/// <param name="includeProperties">Comma separated string of relationship
		/// properties to include.</param>
		/// <returns>The first entity matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		Task<TEntity> GetFirstAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null)
			where TEntity : class, IEntity;

		/// <summary>
		/// Gets an <see cref="IEntity"/> by its identifier.
		/// </summary>
		/// <param name="id">The identity of the entity to retrieve.</param>
		/// <returns>The entity matching the given identifier.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		TEntity GetById<TEntity>(object id)
			where TEntity : class, IEntity;

		/// <summary>
		/// Asynchronously gets an <see cref="IEntity"/> by its identifier.
		/// </summary>
		/// <param name="id">The identity of the entity to retrieve.</param>
		/// <returns>The entity matching the given identifier.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		Task<IEntity> GetByIdAsync<TEntity>(object id)
			where TEntity : class, IEntity;

		/// <summary>
		/// Gets the count of <see cref="IEntity"/> matching a filter.
		/// </summary>
		/// <param name="filter">The filter for the entity count.</param>
		/// <returns>The count of the entities matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		int GetCount<TEntity>(
			Expression<Func<TEntity, bool>> filter = null)
			where TEntity : class, IEntity;

		/// <summary>
		/// Asynchronously gets the count of <see cref="IEntity"/> matching a filter.
		/// </summary>
		/// <param name="filter">The filter for the entity count.</param>
		/// <returns>The count of the entities matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		Task<int> GetCountAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null)
			where TEntity : class, IEntity;

		/// <summary>
		/// Gets whether or not an <see cref="IEntity"/> exists matching a filter.
		/// </summary>
		/// <param name="filter">The filter for entity existence.</param>
		/// <returns><c>true</c> if an entity matches the filter, otherwise <c>false</c>.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		bool Exists<TEntity>(
			Expression<Func<TEntity, bool>> filter = null)
			where TEntity : class, IEntity;

		/// <summary>
		/// Asynchronously gets whether or not an <see cref="IEntity"/> exists matching a filter.
		/// </summary>
		/// <param name="filter">The filter for entity existence.</param>
		/// <returns><c>true</c> if an entity matches the filter, otherwise <c>false</c>.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		Task<bool> ExistsAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null)
			where TEntity : class, IEntity;

	}

}
