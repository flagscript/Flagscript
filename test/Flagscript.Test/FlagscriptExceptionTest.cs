using System;

using Xunit;

namespace Flagscript.Test
{

	/// <summary>
	/// Unit Tests for <see cref="FlagscriptException"/>.
	/// </summary>
    public class FlagscriptExceptionTest
    {

		/// <summary>
		/// Tests that <see cref="FlagscriptException"/> inherits <see cref="Exception"/>.
		/// </summary>
		[Fact]
		public void TestInheritance()
		{

			FlagscriptException flagscriptException = new FlagscriptException();
			Assert.IsAssignableFrom<Exception>(flagscriptException);

		}

	}

}
