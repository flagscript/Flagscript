using System;

namespace Flagscript
{

	/// <summary>
	/// Represents errors that occur during application execution specific to the 
	/// Flagscript framework.
	/// </summary>
	public class FlagscriptException : Exception
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="FlagscriptException"/> class.
		/// </summary>
		public FlagscriptException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FlagscriptException"/> 
		/// class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public FlagscriptException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FlagscriptException"/> 
		/// class with a specified error message and a reference to the inner 
		/// exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, 
		/// or a null reference (<c>Nothing</c> in Visual Basic) if no inner exception is specified.</param>
		public FlagscriptException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

	}

}