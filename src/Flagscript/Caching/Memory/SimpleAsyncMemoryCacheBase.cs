using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Flagscript.Caching.Memory
{

	/// <summary>
	/// Simple async memory cache base.
	/// </summary>
	public abstract class SimpleAsyncMemoryCacheBase<TItem> : SimpleMemoryCacheBase<TItem>, ISimpleAsyncMemoryCache<TItem>
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleAsyncMemoryCacheBase{TCache}"/> class
		/// with no configuration or logging.
		/// </summary>
		protected SimpleAsyncMemoryCacheBase()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleAsyncMemoryCacheBase{TCache}"/> class
		/// with a logging context.
		/// </summary>
		/// <param name="logger">Logger to be used for logging.</param>
		protected SimpleAsyncMemoryCacheBase(ILogger logger) : base(logger)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleAsyncMemoryCacheBase{TCache}"/> class
		/// with specified cache options.
		/// </summary>
		/// <param name="memoryCacheOptions">Memory cache options.</param>
		protected SimpleAsyncMemoryCacheBase(MemoryCacheOptions memoryCacheOptions) : base(memoryCacheOptions)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleAsyncMemoryCacheBase{TCache}"/> class
		/// with specified cache options and logging context.
		/// </summary>
		/// <param name="memoryCacheOptions">Memory cache options.</param>
		/// <param name="logger">Logger to be used for logging.</param>
		protected SimpleAsyncMemoryCacheBase(MemoryCacheOptions memoryCacheOptions, ILogger logger) : base(memoryCacheOptions, logger)
		{
		}

		/// <summary>
		/// Asynchronously returns a cached object from memory for a given key, or creates one, stores
		/// in the cache and returns it. 
		/// </summary>
		/// <param name="identifier">Identifier to be used to look up the object.</param>
		/// <param name="factory">Inline factory function used to create the object.</param>
		/// <returns>Cached <see cref="TItem"/>.</returns>
		public async Task<TItem> GetOrCreateAsync(object identifier, Func<Task<TItem>> factory)
		{

			object cacheKey = GenerateCacheKey(identifier);
			return await MemoryCache.GetOrCreateAsync(cacheKey, entry => factory());

		}

	}

}
