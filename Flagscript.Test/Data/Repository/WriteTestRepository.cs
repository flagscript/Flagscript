using System;

using Flagscript.Data.Repository;

namespace Flagscript.Test.Data.Repository
{

	/// <summary>
	/// A <see cref="EntityFrameworkRepository{TContext}"/> used for unit testing.
	/// </summary>
	public class WriteTestRepository : EntityFrameworkRepository<TestDbContext>
	{

		#region Constructors

		/// <summary>
		/// Initializes an instance of <see cref="WriteTestRepository"/>
		/// with the given <see cref="TestDbContext"/> DbContext.
		/// </summary>
		/// <param name="context">The <see cref="TestDbContext"/> this this repository
		/// will execute on.</param>
		/// <exception cref="ArgumentNullException">If <c>context</c> is null.</exception>
		public WriteTestRepository(TestDbContext context) : base(context)
		{
		}

		#endregion

	}

}
