using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Flagscript.Caching.Memory
{

	/// <summary>
	/// Base implementation for all types of <see cref="ISimpleMemoryCache{TItem}"/>.
	/// </summary>
	public abstract class SimpleMemoryCacheBase<TItem> : ISimpleMemoryCache<TItem>
	{

		/// <summary>
		/// Logger to be used for logging.
		/// </summary>
		/// <value>The logger to be used for logging.</value>
		protected ILogger Logger { get; private set; }

		/// <summary>
		/// The Memory Cache.
		/// </summary>
		/// <value>The memory cache.</value>
		protected internal IMemoryCache MemoryCache { get; set; }

		/// <summary>
		/// The memory cache entry options used when caching values.
		/// </summary>
		/// <value>Memory cache entry options used when caching values.</value>
		protected MemoryCacheEntryOptions MemoryCacheEntryOptions { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleMemoryCacheBase{TCache}"/> class
		/// with no configuration or logging.
		/// </summary>
		protected SimpleMemoryCacheBase() => MemoryCache = new MemoryCache(new MemoryCacheOptions());

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleMemoryCacheBase{TCache}"/> class
		/// with a logging context.
		/// </summary>
		/// <param name="logger">Logger to be used for logging.</param>
		protected SimpleMemoryCacheBase(ILogger logger) : this() => Logger = logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleMemoryCacheBase{TCache}"/> class
		/// with specified cache options.
		/// </summary>
		/// <param name="memoryCacheOptions">Memory cache options.</param>
		protected SimpleMemoryCacheBase(MemoryCacheOptions memoryCacheOptions) => MemoryCache = new MemoryCache(memoryCacheOptions);

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleMemoryCacheBase{TCache}"/> class
		/// with specified cache options and logging context.
		/// </summary>
		/// <param name="memoryCacheOptions">Memory cache options.</param>
		/// <param name="logger">Logger to be used for logging.</param>
		protected SimpleMemoryCacheBase(MemoryCacheOptions memoryCacheOptions, ILogger logger) : this(memoryCacheOptions) => Logger = logger;

		/// <summary>
		/// Generates the cache key for the given object. 
		/// </summary>
		/// <param name="identifier">Identifier used to lookup the object in the cache.</param>
		/// <returns>The cache key to be used for the given object.</returns>
		public abstract object GenerateCacheKey(object identifier);

	}

}
