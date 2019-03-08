using System;

using Xunit;

namespace Flagscript.Extensions.Dotnet.Test
{

	/// <summary>
	/// Unit tests for <see cref="EnumExtensions"/>.
	/// </summary>
	public class EnumExtensionsTest
	{

		#region Test methods

		/// <summary>
		/// Tests <see cref="EnumExtensions.GetEnumDescription{T}(T)" /> with a 
		/// non-enum type.
		/// </summary>
		[Fact]
		public void TestGetEnumDescriptionNonEnumType()
		{
			TestStruct testStruct = new TestStruct();
			try
			{
				string noDesc = testStruct.GetEnumDescription();
			}
			catch (ArgumentException ae)
			{
				Assert.Equal("enumValue", ae.ParamName);
			}
		}

		/// <summary>
		/// Tests <see cref="EnumExtensions.GetEnumDescription{T}(T)" /> for a 
		/// enum member with no description attribute.
		/// </summary>
		[Fact]
		public void TestGetEnumDescriptionNoDescriptionAttribute()
		{

			Assert.Equal("ValueOne", TestEnum.ValueOne.GetEnumDescription());

		}

		/// <summary>
		/// Tests <see cref="EnumExtensions.GetEnumDescription{T}(T)" /> for a 
		/// enum member with a description attribute.
		/// </summary>
		[Fact]
		public void TestGetEnumDescriptionWithDescriptionAttribute()
		{

			Assert.Equal("TwoValue", TestEnum.ValueTwo.GetEnumDescription());

		}

		#endregion

	}

	/// <summary>
	/// Test struct for <see cref="EnumExtensionsTest.TestGetEnumDescriptionNonEnumType"/>.
	/// </summary>
	public struct TestStruct
	{

	}

}
