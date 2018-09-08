namespace Flagscript.Data.Entity
{

	/// <summary>
	/// Base model tagging interface for all entities in the Flagscript Framework.
	/// </summary>
	/// <remarks>
	/// As a convention, all entities in the Flagscript Framework have a name 
	/// value. This should be unique in most contexts.
	/// </remarks>
	public interface INamedEntity
	{

		/// <summary>
		/// Gets or sets the name identifier of the entity.
		/// </summary>
		/// <value>The name identifier of the entity..</value>
		string Name { get; set; }

	}

}
