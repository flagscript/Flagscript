using System.Text;

using Flagscript.Caching.Memory;

namespace Flagscript.Unit.Tests.Caching.Memory
{

	/// <summary>
	/// Test Helper for <see cref="SimpleAsyncMemoryCacheBaseTest"/>.
	/// </summary>
	public class AsyncMemoryCache : SimpleAsyncMemoryCacheBase<StringBuilder>
	{

		/// <summary>
		/// Generates the cache key.
		/// </summary>
		/// <returns>The cache key.</returns>
		/// <param name="identifier">Identifier.</param>
		public override object GenerateCacheKey(object identifier) => identifier;

	}

}
