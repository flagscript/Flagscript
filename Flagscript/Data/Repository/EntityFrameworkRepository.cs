using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Flagscript.Data.Entity;

namespace Flagscript.Data.Repository
{

	/// <summary>
	/// A generic repository base class for entity framework read and write operations.
	/// </summary>
	/// <typeparam name="TContext">The type of the <see cref="DbContext"/> this 
	/// repository will execute on.</typeparam>
	public class EntityFrameworkRepository<TContext> : EntityFrameworkReadOnlyRepository<TContext>, IRepository
		where TContext : DbContext
	{

		#region Constructors

		/// <summary>
		/// Initializes an instance of <see cref="EntityFrameworkRepository{TContext}"/>
		/// with the given <see cref="DbContext"/>.
		/// </summary>
		/// <param name="context">The <see cref="DbContext"/> this this repository
		/// will execute on.</param>
		/// <exception cref="ArgumentNullException">If <c>context</c> is null.</exception>
		public EntityFrameworkRepository(TContext context) : base(context)
		{
		}

		#endregion

		#region Contract

		/// <summary>
		/// Marks an entity to be created.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <param name="createdBy">The principal who is creating the entity.</param>
		/// <typeparam name="TEntity">The type of the <see cref="IEntity"/> to create.</typeparam>
		public virtual void Create<TEntity>(TEntity entity, string createdBy = null)
			where TEntity : class, IEntity
		{
			entity.CreatedDate = DateTime.UtcNow;
			entity.CreatedBy = createdBy;
			context.Set<TEntity>().Add(entity);
		}

		/// <summary>
		/// Marks an entity to be updated.
		/// </summary>
		/// <param name="entity">The entity to update with its updated values.</param>
		/// <param name="modifiedBy">The principal who modified the entity.</param>
		/// <typeparam name="TEntity">The type of the <see cref="IEntity"/> to update.</typeparam>
		public virtual void Update<TEntity>(TEntity entity, string modifiedBy = null)
			where TEntity : class, IEntity
		{
			entity.ModifiedDate = DateTime.UtcNow;
			entity.ModifiedBy = modifiedBy;
			context.Set<TEntity>().Attach(entity);
			context.Entry(entity).State = EntityState.Modified;
		}

		/// <summary>
		/// Marks an entity for deletion by its identifier.
		/// </summary>
		/// <param name="id">The identifier of the entity to delete.</param>
		/// <typeparam name="TEntity">The type of the <see cref="IEntity"/> to delete.</typeparam>
		public virtual void Delete<TEntity>(object id)
			where TEntity : class, IEntity
		{
			TEntity entity = context.Set<TEntity>().Find(id);
			Delete(entity);
		}

		/// <summary>
		/// Marks an entity for deletion.
		/// </summary>
		/// <param name="entity">The entity to mark for deletion.</param>
		/// <typeparam name="TEntity">The type of the <see cref="IEntity"/> to delete.</typeparam>
		public virtual void Delete<TEntity>(TEntity entity)
			where TEntity : class, IEntity
		{
			var dbSet = context.Set<TEntity>();
			if (context.Entry(entity).State == EntityState.Detached)
			{
				dbSet.Attach(entity);
			}
			dbSet.Remove(entity);
		}

		/// <summary>
		/// Saves all entities marked for creation, update or deletion to the data store.
		/// </summary>
		/// <returns>The number of all entries written to the data store.</returns>
		public virtual int Save()
		{

			ValidateEntitiesOrThrowException();
			return context.SaveChanges();
		}

		/// <summary>
		/// Asynchronously saves all entities marked for creation, update or deletion to the data store.
		/// </summary>
		/// <returns>The number of all entries written to the data store.</returns>
		public virtual async Task<int> SaveAsync()
		{

			ValidateEntitiesOrThrowException();
			return await context.SaveChangesAsync();
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Validates the <see cref="DbContext"/> changetracker for validation 
		/// errors. 
		/// </summary>
		/// <exception cref="FlagscriptDataException">If one of the entities is not valid.</exception>
		protected virtual void ValidateEntitiesOrThrowException()
		{

			var entities = context.ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
				.Select(e => e.Entity);

			IList<ValidationResult> validationErrors = new List<ValidationResult>();

			foreach (var entity in entities)
			{
				Validator.TryValidateObject(entity, new ValidationContext(entity), validationErrors, true);
			}

			if (validationErrors.Any())
			{
				var errorMessages = validationErrors
					.SelectMany(ve => ve.ErrorMessage);
				var fullErrorMessage = string.Join("; ", errorMessages);
				throw new FlagscriptDataException($"Entity Validation Exceptions: Errors => {fullErrorMessage}");
			}

		}

		#endregion

	}

}
