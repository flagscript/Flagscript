using System;

namespace Flagscript.Data.Entity
{

	/// <summary>
	/// Interface defining common fields for all entities in the Flagscript 
	/// Framework.
	/// </summary>
	public interface IEntity : INamedEntity
	{

		/// <summary>
		/// Gets or sets the identifier of the entity.
		/// </summary>
		/// <value>The identifier of the entity.</value>
		object Id { get; }

		/// <summary>
		/// Gets or sets the timestamp the entity was created.
		/// </summary>
		/// <value>The timestamp the entity was created.</value>
		DateTime CreatedDate { get; set; }

		/// <summary>
		/// Gets or sets the timestamp the entity was last modified.
		/// </summary>
		/// <value>The timestamp the entity was last modified.</value>
		DateTime? ModifiedDate { get; set; }

		/// <summary>
		/// Gets or sets the name of the principal who created the entity.
		/// </summary>
		/// <value>The name of the principal who created the entity.</value>
		string CreatedBy { get; set; }

		/// <summary>
		/// Gets or sets the name of the principal who last modified the entity.
		/// </summary>
		/// <value>The name of the principal who last modified the entity.</value>
		string ModifiedBy { get; set; }

		/// <summary>
		/// Gets or sets the version of the entity used for concurrency.
		/// </summary>
		/// <value>Version of the entity used for concurrency.</value>
		byte[] Version { get; set; }

	}

	/// <summary>
	/// Base interface for all entities in the Flagscript Framework which types
	/// the entities <c>Id</c>.
	/// </summary>
	public interface IEntity<T> : IEntity
	{

		/// <summary>
		/// The typed identifier of the entity.
		/// </summary>
		/// <value>The typed identifier of the entity.</value>
		new T Id { get; }

	}

}
