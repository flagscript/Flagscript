using System.ComponentModel;

namespace Flagscript.Extensions.Dotnet.Test
{

	/// <summary>
	/// Test enumeration for <see cref="EnumExtensionsTest"/>.
	/// </summary>
	public enum TestEnum
	{

		/// <summary>
		/// Enum member with no description attribute. 
		/// </summary>
		ValueOne,

		/// <summary>
		/// Enum member with description attribute.
		/// </summary>
		[Description("TwoValue")]
		ValueTwo

	}

}
