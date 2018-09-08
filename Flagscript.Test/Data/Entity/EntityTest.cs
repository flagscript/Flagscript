using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flagscript.Data.Entity;

namespace Flagscript.Test.Data.Entity
{

	/// <summary>
	/// Unit tests for the Flagscript.Data.Entity namespace.
	/// </summary>
	[TestClass]
	public class EntityTest
	{

		/// <summary>
		/// Validates base structures when using <see cref="Entity{T}"/>.
		/// </summary>
		[TestMethod]
		public void StructuralTests()
		{
			IntTestEntity testEntity = new IntTestEntity();
			Assert.IsInstanceOfType(testEntity, typeof(IEntity),
				"Entity{T} is not IEntity.");
			Assert.IsInstanceOfType(testEntity, typeof(INamedEntity),
				"Entity{T} is not INamedEntity.");
		}

		/// <summary>
		/// Tests states of entity creation. 
		/// </summary>
		[TestMethod]
		public void StateTests()
		{
			IntTestEntity testEntity = new IntTestEntity();
			DateTime initCreated = testEntity.CreatedDate;
			Assert.IsTrue(initCreated > DateTime.UtcNow.AddMinutes(-1),
				"Entity{T} is not initing CreatedDate correctly" );
		}

		/// <summary>
		/// Validates id types and base id typing.
		/// </summary>
		[TestMethod]
		public void TypedTests()
		{
			GuidTestEntity guidTestEntity = new GuidTestEntity();

			// Guid id type tests
			object guidEntityId = guidTestEntity.Id;
			Assert.AreEqual(typeof(Guid), guidEntityId.GetType(),
				"GuidTestEntity.ID is not System.Guid.");
			IEntity castEntity = guidTestEntity as IEntity;
			object castId = castEntity.Id;
			Assert.AreEqual(typeof(Guid), castId.GetType(),
				"IEntity.ID is not System.Guid.");
		}


	}

}
