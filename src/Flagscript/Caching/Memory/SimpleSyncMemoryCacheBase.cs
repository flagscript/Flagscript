using System;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Flagscript.Caching.Memory
{

	/// <summary>
	/// Simple sync memory cache base.
	/// </summary>
	public abstract class SimpleSyncMemoryCacheBase<TItem> : SimpleMemoryCacheBase<TItem>, ISimpleSyncMemoryCache<TItem>
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleSyncMemoryCacheBase{TCache}"/> class
		/// with no configuration or logging.
		/// </summary>
		protected SimpleSyncMemoryCacheBase()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleSyncMemoryCacheBase{TCache}"/> class
		/// with a logging context.
		/// </summary>
		/// <param name="logger">Logger to be used for logging.</param>
		protected SimpleSyncMemoryCacheBase(ILogger logger) : base(logger)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleSyncMemoryCacheBase{TCache}"/> class
		/// with specified cache options.
		/// </summary>
		/// <param name="memoryCacheOptions">Memory cache options.</param>
		protected SimpleSyncMemoryCacheBase(MemoryCacheOptions memoryCacheOptions) : base(memoryCacheOptions)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleSyncMemoryCacheBase{TCache}"/> class
		/// with specified cache options and logging context.
		/// </summary>
		/// <param name="memoryCacheOptions">Memory cache options.</param>
		/// <param name="logger">Logger to be used for logging.</param>
		protected SimpleSyncMemoryCacheBase(MemoryCacheOptions memoryCacheOptions, ILogger logger) : base(memoryCacheOptions, logger)
		{
		}

		/// <summary>
		/// Returns a cached object from memory for a given key, or creates one, stores
		/// in the cache and returns it. 
		/// </summary>
		/// <param name="identifier">Identifier to be used to look up the object.</param>
		/// <param name="factory">Inline factory function used to create the object.</param>
		/// <returns>Cached <see cref="TItem"/>.</returns>
		public TItem GetOrCreate(object identifier, Func<TItem> factory)
		{

			object cacheKey = GenerateCacheKey(identifier);
			return MemoryCache.GetOrCreate(cacheKey, entry => factory());

		}

	}

}
