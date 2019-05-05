using System.Text;
using System.Threading.Tasks;

using Xunit;

using Flagscript.Caching.Memory;

namespace Flagscript.Unit.Tests.Caching.Memory
{

	/// <summary>
	/// Unit tests for <see cref="SimpleAsyncMemoryCacheBase{TItem}"/>.
	/// </summary>
	public class SimpleAsyncMemoryCacheBaseTest
	{

		/// <summary>
		/// Ensures we actually cache the value.
		/// </summary>
		[Fact]
		public async Task TestGetOrCreateAsync()
		{

			AsyncMemoryCache memCache = new AsyncMemoryCache();

			var cacheString = "myCacheString";
			var cacheStringKey = memCache.GenerateCacheKey(cacheString);

			// Assert not in cache.
			memCache.MemoryCache.TryGetValue(cacheStringKey, out object testGetString);
			Assert.Null(testGetString);

			// Assert created in cache.
			var outCacheStringCreate = await memCache.GetOrCreateAsync(
				cacheStringKey,
				() => Task.FromResult(new StringBuilder("myCacheString"))
			);
			Assert.NotNull(outCacheStringCreate);

			// Try to get the same.
			var outCacheStringGet = await memCache.GetOrCreateAsync(
				cacheStringKey, 
				() => Task.FromResult(new StringBuilder("myCacheString"))
			);
			Assert.Same(outCacheStringCreate, outCacheStringGet);

			// Remove and generate new
			memCache.MemoryCache.Remove(cacheStringKey);
			var outCacheStringCreateNew = memCache.GetOrCreateAsync(
				cacheStringKey, 
				() => Task.FromResult(new StringBuilder("myCacheString"))
			);
			Assert.NotSame(outCacheStringGet, outCacheStringCreateNew);

		}


	}

}
