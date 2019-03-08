using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Flagscript.Extensions.Dotnet
{

	/// <summary>
	/// Extensions for <see cref="Enum"/>.
	/// </summary>
	public static class EnumExtensions
	{

		/// <summary>
		/// Returns the best <see cref="string"/> description for an enum member.
		/// </summary>
		/// <returns>The description attribute assigned to the enum, or the to string of the enum..</returns>
		/// <param name="enumValue">Enum value.</param>
		/// <typeparam name="T">The type parameter.</typeparam>
		public static string GetEnumDescription<T>(this T enumValue) where T : struct
		{

			Type type = enumValue.GetType();

			// This one's only for enums
			if (!type.IsEnum)
			{
				throw new ArgumentException(
					"Must be called on an enumeration type.",
					nameof(enumValue)
				);
			}

			// Tries to find a description attribute for a potential friendly
			// name
			MemberInfo[] memberInfo = type.GetMember(enumValue.ToString());
			if (memberInfo != null && memberInfo.Any())
			{

				object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (attrs != null && attrs.Any())
				{
					// Pull out the description value. 
					return ((DescriptionAttribute)attrs[0]).Description;
				}

			}

			// If we have no description attribute, just return the ToString of the enum.
			return enumValue.ToString();

		}


	}

}