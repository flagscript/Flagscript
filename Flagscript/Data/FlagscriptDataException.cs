﻿using System;

namespace Flagscript
{

	/// <summary>
	/// Represents errors that occur during application execution specific to data
	/// layers in the Flagscript framework.
	/// </summary>
	public class FlagscriptDataException : FlagscriptException
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="FlagscriptDataException"/> class.
		/// </summary>
		public FlagscriptDataException() : base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FlagscriptDataException"/> 
		/// class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public FlagscriptDataException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FlagscriptDataException"/> 
		/// class with a specified error message and a reference to the inner 
		/// exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, 
		/// or a null reference (<c>Nothing</c> in Visual Basic) if no inner exception is specified.</param>
		public FlagscriptDataException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

	}

}