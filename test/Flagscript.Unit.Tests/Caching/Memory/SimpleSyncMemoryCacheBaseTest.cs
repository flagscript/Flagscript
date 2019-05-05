using System.Text;

using Xunit;

using Flagscript.Caching.Memory;

namespace Flagscript.Unit.Tests.Caching.Memory
{

	/// <summary>
	/// Unit tests for <see cref="SimpleSyncMemoryCacheBase{TItem}"/>.
	/// </summary>
	public class SimpleSyncMemoryCacheBaseTest
	{

		/// <summary>
		/// Ensures we actually cache the value.
		/// </summary>
		[Fact]
		public void TestGetOrCreate()
		{

			SyncMemoryCache memCache = new SyncMemoryCache();

			var cacheString = "myCacheString";
			var cacheStringKey = memCache.GenerateCacheKey(cacheString);

			// Assert not in cache.
			memCache.MemoryCache.TryGetValue(cacheStringKey, out object testGetString);
			Assert.Null(testGetString);

			// Assert created in cache.
			var outCacheStringCreate = memCache.GetOrCreate(cacheStringKey, () => new StringBuilder("myCacheString"));
			Assert.NotNull(outCacheStringCreate);

			// Try to get the same.
			var outCacheStringGet = memCache.GetOrCreate(cacheStringKey, () => new StringBuilder("myCacheString"));
			Assert.Same(outCacheStringCreate, outCacheStringGet);

			// Remove and generate new
			memCache.MemoryCache.Remove(cacheStringKey);
			var outCacheStringCreateNew = memCache.GetOrCreate(cacheStringKey, () => new StringBuilder("myCacheString"));
			Assert.NotSame(outCacheStringGet, outCacheStringCreateNew);

		}


	}

}
