namespace Flagscript.Caching.Memory
{

	/// <summary>
	/// Very simple typed object memory cache for use in the Flagscript framework.
	/// </summary>
	/// <typeparam name="TItem">Type of the object cached.</typeparam>
	public interface ISimpleMemoryCache<TItem>
	{

		/// <summary>
		/// Generates the cache key given an object identifier.
		/// </summary>
		/// <returns>The cache key for the cache identifier.</returns>
		/// <param name="identifier">The object identifier for the key.</param>
		object GenerateCacheKey(object identifier);

	}

}
