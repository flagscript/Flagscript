using System;

namespace Flagscript.Caching.Memory
{

	/// <summary>
	/// Very simple typed object memory cache for use in the Flagscript framework, where typed
	/// object creation is synchronous.
	/// </summary>
	/// <typeparam name="TItem">Type of the object cached.</typeparam>
	public interface ISimpleSyncMemoryCache<TItem> : ISimpleMemoryCache<TItem>
	{

		/// <summary>
		/// Returns a cached object from memory for a given key, or creates one, stores
		/// in the cache and returns it. 
		/// </summary>
		/// <param name="identifier">Identifier to be used to look up the object.</param>
		/// <param name="factory">Inline factory function used to create the object.</param>
		/// <returns>Cached <see cref="TItem"/>.</returns>
		TItem GetOrCreate(object identifier, Func<TItem> factory);

	}

}
