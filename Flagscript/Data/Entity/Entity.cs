using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flagscript.Data.Entity.Infrastructure
{

	/// <summary>
	/// Entity.
	/// </summary>
	public abstract class Entity<T> : IEntity<T>
	{

		#region Fields 

		/// <summary>
		/// Backing field for <see cref="CreatedDate"/>.
		/// </summary>
		private DateTime? createdDate;

		#endregion

		#region Properties 

		/// <summary>
		/// The typed identifier of the entity.
		/// </summary>
		/// <value>The typed identifier of the entity.</value>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public T Id { get; set; }

		/// <summary>
		/// Maps the base <see cref="IEntity.Id"/> to <see cref="Id"/>.
		/// </summary>
		/// <value>The base <see cref="IEntity.Id"/> mapped to <see cref="Id"/>.</value>
		object IEntity.Id => Id;

		/// <summary>
		/// Gets or sets the name identifier of the entity.
		/// </summary>
		/// <value>The name identifier of the entity..</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the timestamp the entity was created.
		/// </summary>
		/// <value>The timestamp the entity was created.</value>
		[DataType(DataType.DateTime)]
		public DateTime CreatedDate
		{
			get => createdDate ?? DateTime.UtcNow;
			set => createdDate = value;
		}

		/// <summary>
		/// Gets or sets the timestamp the entity was last modified.
		/// </summary>
		/// <value>The timestamp the entity was last modified.</value>
		[DataType(DataType.DateTime)]
		public DateTime? ModifiedDate { get; set; }

		/// <summary>
		/// Gets or sets the name of the principal who created the entity.
		/// </summary>
		/// <value>The name of the principal who created the entity.</value>
		public string CreatedBy { get; set; }

		/// <summary>
		/// Gets or sets the name of the principal who last modified the entity.
		/// </summary>
		/// <value>The name of the principal who last modified the entity.</value>
		public string ModifiedBy { get; set; }

		/// <summary>
		/// Gets or sets the version of the entity used for concurrency.
		/// </summary>
		/// <value>Version of the entity used for concurrency.</value>
		[Timestamp]
		public byte[] Version { get; set; }

		#endregion

	}

}
