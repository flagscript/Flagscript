using System;

using Flagscript.Data.Entity;

namespace Flagscript.Test.Data.Entity
{

	/// <summary>
	/// Test entity with <see cref="Guid"/> identifier.
	/// </summary>
	public class GuidTestEntity : Entity<Guid>
	{
	}

	/// <summary>
	/// Test entity with <see cref="int"/> identifier.
	/// </summary>
	public class IntTestEntity : Entity<int>
	{
	}

}
