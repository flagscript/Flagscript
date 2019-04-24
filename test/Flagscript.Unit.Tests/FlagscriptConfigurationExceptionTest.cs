using Xunit;

namespace Flagscript.Unit.Tests
{

	/// <summary>
	/// Unit tests for <see cref="FlagscriptConfigurationException"/>.
	/// </summary>
	public class FlagscriptConfigurationExceptionTest
	{

		/// <summary>
		/// Tests that <see cref="FlagscriptConfigurationException"/> is of type 
		/// <see cref="FlagscriptException"/>.
		/// </summary>
		public void TestInheritance()
		{

			FlagscriptConfigurationException fce = new FlagscriptConfigurationException();
			Assert.IsAssignableFrom<FlagscriptException>(fce);

		}

	}

}
