using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Flagscript.Data.Entity;

namespace Flagscript.Data.Repository
{

	/// <summary>
	/// A generic repository base class for entity framework read only operations.
	/// </summary>
	/// <typeparam name="TContext">The type of the <see cref="DbContext"/> this 
	/// repository will execute on.</typeparam>
	public class EntityFrameworkReadOnlyRepository<TContext> : IReadOnlyRepository
		where TContext : DbContext
	{

		#region Fields

		/// <summary>
		/// The <see cref="DbContext"/> this repository will operate on.
		/// </summary>
		protected readonly TContext context;

		#endregion

		#region Constructors 

		/// <summary>
		/// Initializes an instance of <see cref="EntityFrameworkReadOnlyRepository{TContext}"/>
		/// with the given <see cref="DbContext"/>.
		/// </summary>
		/// <param name="context">The <see cref="DbContext"/> this this repository
		/// will execute on.</param>
		/// <exception cref="ArgumentNullException">If <c>context</c> is null.</exception>
		public EntityFrameworkReadOnlyRepository(TContext context)
		{
			this.context = context ?? throw new ArgumentNullException(nameof(context));
		}

		#endregion

		#region Contracts 

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
		public virtual IEnumerable<TEntity> GetAll<TEntity>(
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null,
			int? skip = null,
			int? take = null)
			where TEntity : class, IEntity
		{
			return GetQueryable(null, orderBy, includeProperties, skip, take)
				.ToList();
		}

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
		public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null,
			int? skip = null,
			int? take = null)
			where TEntity : class, IEntity
		{
			return await GetQueryable(null, orderBy, includeProperties, skip, take)
				.ToListAsync();
		}

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
		public virtual IEnumerable<TEntity> Get<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null,
			int? skip = null,
			int? take = null)
			where TEntity : class, IEntity
		{
			return GetQueryable(filter, orderBy, includeProperties, skip, take)
				.ToList();
		}

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
		public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null,
			int? skip = null,
			int? take = null)
			where TEntity : class, IEntity
		{
			return await GetQueryable(filter, orderBy, includeProperties, skip, take)
				.ToListAsync();
		}

		/// <summary>
		/// Gets the single <see cref="IEntity"/> of a given type matching a filter.
		/// </summary>
		/// <param name="filter">The expression used to filter the entity.</param>
		/// <param name="includeProperties">Comma separated string of relationship
		/// properties to include.</param>
		/// <returns>The single entity matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		public virtual TEntity GetOne<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			string includeProperties = null)
			where TEntity : class, IEntity
		{
			return GetQueryable(filter, null, includeProperties)
				.SingleOrDefault();
		}

		/// <summary>
		/// Asynchronously gets the single <see cref="IEntity"/> of a given type matching a filter.
		/// </summary>
		/// <param name="filter">The expression used to filter the entity.</param>
		/// <param name="includeProperties">Comma separated string of relationship
		/// properties to include.</param>
		/// <returns>The single entity matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		public virtual async Task<TEntity> GetOneAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			string includeProperties = null)
			where TEntity : class, IEntity
		{
			return await GetQueryable(filter, null, includeProperties).SingleOrDefaultAsync();
		}

		/// <summary>
		/// Gets the first <see cref="IEntity"/> of a given type matching a filter.
		/// </summary>
		/// <param name="filter">The expression used to filter the entity.</param>
		/// <param name="includeProperties">Comma separated string of relationship
		/// properties to include.</param>
		/// <returns>The first entity matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		public virtual TEntity GetFirst<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null)
			where TEntity : class, IEntity
		{
			return GetQueryable(filter, orderBy, includeProperties)
				.FirstOrDefault();
		}

		/// <summary>
		/// Asynchronously gets the first <see cref="IEntity"/> of a given type matching a filter.
		/// </summary>
		/// <param name="filter">The expression used to filter the entity.</param>
		/// <param name="includeProperties">Comma separated string of relationship
		/// properties to include.</param>
		/// <returns>The first entity matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		public virtual async Task<TEntity> GetFirstAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null)
			where TEntity : class, IEntity
		{
			return await GetQueryable(filter, orderBy, includeProperties)
				.FirstOrDefaultAsync();
		}

		/// <summary>
		/// Gets an <see cref="IEntity"/> by its identifier.
		/// </summary>
		/// <param name="id">The identity of the entity to retrieve.</param>
		/// <returns>The entity matching the given identifier.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		public virtual TEntity GetById<TEntity>(object id)
			where TEntity : class, IEntity
		{
			return context.Set<TEntity>().Find(id);
		}

		/// <summary>
		/// Asynchronously gets an <see cref="IEntity"/> by its identifier.
		/// </summary>
		/// <param name="id">The identity of the entity to retrieve.</param>
		/// <returns>The entity matching the given identifier.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		public virtual async Task<IEntity> GetByIdAsync<TEntity>(object id)
			where TEntity : class, IEntity
		{
			return await context.Set<TEntity>().FindAsync(id);
		}

		/// <summary>
		/// Gets the count of <see cref="IEntity"/> matching a filter.
		/// </summary>
		/// <param name="filter">The filter for the entity count.</param>
		/// <returns>The count of the entities matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		public virtual int GetCount<TEntity>(
			Expression<Func<TEntity, bool>> filter = null)
			where TEntity : class, IEntity
		{
			return GetQueryable(filter).Count();
		}

		/// <summary>
		/// Asynchronously gets the count of <see cref="IEntity"/> matching a filter.
		/// </summary>
		/// <param name="filter">The filter for the entity count.</param>
		/// <returns>The count of the entities matching the filter.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		public virtual async Task<int> GetCountAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null)
			where TEntity : class, IEntity
		{
			return await GetQueryable(filter).CountAsync();
		}

		/// <summary>
		/// Gets whether or not an <see cref="IEntity"/> exists matching a filter.
		/// </summary>
		/// <param name="filter">The filter for entity existence.</param>
		/// <returns><c>true</c> if an entity matches the filter, otherwise <c>false</c>.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		public virtual bool Exists<TEntity>(
			Expression<Func<TEntity, bool>> filter = null)
			where TEntity : class, IEntity
		{
			return GetQueryable(filter).Any();
		}

		/// <summary>
		/// Asynchronously gets whether or not an <see cref="IEntity"/> exists matching a filter.
		/// </summary>
		/// <param name="filter">The filter for entity existence.</param>
		/// <returns><c>true</c> if an entity matches the filter, otherwise <c>false</c>.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		public virtual async Task<bool> ExistsAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null)
			where TEntity : class, IEntity
		{
			return await GetQueryable(filter).AnyAsync();
		}

		#endregion

		#region Helpers

		/// <summary>
		/// Helper method for entity framework repository methods to obtain 
		/// an <see cref="IQueryable"/> matching various conditions.
		/// </summary>
		/// <param name="filter">Filter expression for the entity query.</param>
		/// <param name="orderBy">Function used to order the entity query.</param>
		/// <param name="includeProperties">Include properties.</param>
		/// <param name="skip">Optional number of entities to skip.</param>
		/// <param name="take">Optional number of entities to take.</param>
		/// <returns>The queryable.</returns>
		/// <typeparam name="TEntity">The <see cref="IEntity"/> type.</typeparam>
		protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null,
			int? skip = null,
			int? take = null)
			where TEntity : class, IEntity
		{
			includeProperties = includeProperties ?? string.Empty;
			IQueryable<TEntity> query = context.Set<TEntity>();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split
			         (new char[','], StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			if (skip.HasValue)
			{
				query = query.Skip(skip.Value);
			}

			if (take.HasValue)
			{
				query = query.Take(take.Value);
			}

			return query;

		}

		#endregion

	}
}
