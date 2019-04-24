using System;

using Xunit;

namespace Flagscript.Unit.Tests
{

	/// <summary>
	/// Unit tests for <see cref="FlagscriptException"/>.
	/// </summary>
	public class FlagscriptExceptionTest
	{

		/// <summary>
		/// Tests that <see cref="FlagscriptException"/> is of type <see cref="Exception"/>.
		/// </summary>
		public void TestInheritance()
		{
		
			FlagscriptException flagscriptException = new FlagscriptException();
			Assert.IsAssignableFrom<Exception>(flagscriptException);

		}

	}

}
