using System;
using System.Collections.Generic;
using System.Text;

namespace Flagscript.Caching.Memory
{

	/// <summary>
	/// Very simple typed object memory cache for use in the Flagscript framework.
	/// </summary>
	/// <typeparam name="TCache">Type of the object cached.</typeparam>
	public interface ISimpleMemoryCache<TCache> where TCache : class
	{
		
		/// <summary>
		/// Returns a cached object from memory for a given key, or creates one, stores
		/// in the cache and returns it. 
		/// </summary>
		/// <param name="identifier">Identifier to be used to look up the object.</param>
		/// <param name="factory">Inline factory function used to create the object.</param>
		/// <returns></returns>
		TCache GetOrCreate(object identifier, Func<TCache> factory);

		/// <summary>
		/// Generates the cache key for the given object. 
		/// </summary>
		/// <param name="identifier">Identifier used to lookup the object in the cache.</param>
		/// <returns>The cache key to be used for the given object.</returns>
		string GenerateCacheKey(object identifier);
	}

}
