using System;

using Flagscript.Data.Repository;

namespace Flagscript.Test.Data.Repository
{

	/// <summary>
	/// A <see cref="EntityFrameworkReadOnlyRepository{TContext}"/> used for unit
	/// testing.
	/// </summary>
	public class ReadOnlyTestRepository : EntityFrameworkReadOnlyRepository<TestDbContext>
	{

		#region Constructors

		/// <summary>
		/// Initializes an instance of <see cref="ReadOnlyTestRepository"/>
		/// with the given <see cref="TestDbContext"/> DbContext.
		/// </summary>
		/// <param name="context">The <see cref="TestDbContext"/> this this repository
		/// will execute on.</param>
		/// <exception cref="ArgumentNullException">If <c>context</c> is null.</exception>
		public ReadOnlyTestRepository(TestDbContext context) : base(context)
		{
		}

		#endregion

	}

}
