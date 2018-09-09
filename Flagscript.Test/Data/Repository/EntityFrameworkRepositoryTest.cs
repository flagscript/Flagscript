using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flagscript.Test.Data.Repository
{

	/// <summary>
	/// Entity framework write repository tests.
	/// </summary>
	[TestClass]
	public class EntityFrameworkRepositoryTest
	{

		#region Static Properties

		/// <summary>
		/// Read only repo for modification testing.
		/// </summary>
		/// <value>Read only repo.</value>
		private static ReadOnlyTestRepository ReadTestRepo { get; set; }

		/// <summary>
		/// "Secondary" DbContext to use for testing.
		/// </summary>
		/// <value>The read db context.</value>
		private static TestDbContext ReadDbContext { get; set; }

		#endregion

		#region Setup/Teardown

		/// <summary>
		/// Initializes objects used for all tests.
		/// </summary>
		[ClassInitialize]
		public static void TestClassInit(TestContext context)
		{
			Console.WriteLine($"Test Class Initialize before {context.TestName}");
			TestDbContext.SetupDataStore();
			ReadDbContext = new TestDbContext();
			ReadTestRepo = new ReadOnlyTestRepository(ReadDbContext);
		}

		/// <summary>
		/// Cleans up objects used for all tests.
		/// </summary>
		[ClassCleanup]
		public static void TestClassTeardown()
		{
			ReadTestRepo = null;
			ReadDbContext.Dispose();
			ReadDbContext = null;
		}

		#endregion

		#region Test Cases

		/// <summary>
		/// Tests the repository create functionality.
		/// </summary>
		[TestMethod]
		public void TestCreate()
		{

			// Mark Create
			using (TestDbContext writeCtx = new TestDbContext())
			{

				WriteTestRepository writeRepo = new WriteTestRepository(writeCtx);

				var newColor = new Color { Name = "Dark Salmon", Hex = "#e9967a" };
				writeRepo.Create(newColor, "Flagscript Unit Test");

				// Validate not yet created. 
				Assert.IsFalse(
					ReadTestRepo.Exists<Color>(c => c.Name == "Dark Salmon"),
					"Create is not a mark only operation"
				);

				// Save and validate.
				writeRepo.Save();
				Assert.IsTrue(
					ReadTestRepo.Exists<Color>(c => c.Name == "Dark Salmon"),
					"New item does not exist after save."
				);
				Assert.IsNotNull(
					ReadTestRepo.GetOne<Color>(c => c.Name == "Dark Salmon").CreatedBy,
					"Created by not set"
				);

			}

			// Test constraint
			using (TestDbContext writeCtx = new TestDbContext())
			{

				WriteTestRepository writeRepo = new WriteTestRepository(writeCtx);

				try
				{
					var existsColor = new Color { Id = 2, Name = "Sneak King" };
					writeRepo.Create(existsColor);
					writeRepo.Save();
					Assert.Fail("Create not doing constraints");
				}
				catch (DbUpdateException) { } // expected

			}

		}

		/// <summary>
		/// Tests the repository update functionality.
		/// </summary>
		[TestMethod]
		public void TestUpdate()
		{

			// Update non-existant
			using (TestDbContext writeCtx = new TestDbContext())
			{

				WriteTestRepository writeRepo = new WriteTestRepository(writeCtx);

				var newColor = new Color { Id = 33, Name = "Green 40", Hex = "#e8f5e9" };
				try
				{
					writeRepo.Update(newColor, "Flagscript Unit Test");
					writeRepo.Save();
				}
				catch (DbUpdateConcurrencyException) { } // Expected

			}
			Assert.IsFalse(ReadTestRepo.Exists<Color>(c => c.Id == 33), "Somehow non-exist stuck");

			// Create and update
			using (TestDbContext writeCtx = new TestDbContext())
			{

				WriteTestRepository writeRepo = new WriteTestRepository(writeCtx);

				var newColor = new Color { Id = 35, Name = "Green 40", Hex = "#e8f5e9" };
				writeRepo.Create(newColor, "Flagscript Unit Test");
				writeRepo.Save();
				Assert.IsTrue(ReadTestRepo.Exists<Color>(c => c.Id == 35), "Couldn't create new color");

				newColor.Name = "Green 50";
				writeRepo.Update(newColor, "Flagscript Unit Test");

				// Check just marked
				var isItMarked = ReadTestRepo.GetOne<Color>(c => c.Id == 35);
				Assert.AreEqual("Green 40", isItMarked.Name, "Update without update");
				Assert.IsTrue(string.IsNullOrEmpty(isItMarked.ModifiedBy), "Modified by premature");
				ReadDbContext.Entry(isItMarked).State = EntityState.Detached;

				// Update and test
				writeRepo.Save();
				var refresh = ReadTestRepo.GetOne<Color>(c => c.Id == 35);
				Assert.AreEqual("Green 50", refresh.Name, "Didn't update");
				Assert.IsFalse(string.IsNullOrWhiteSpace(refresh.ModifiedBy), "Modified by didn't take");

			}

		}

		/// <summary>
		/// Test the delete with id method. Async just to run through there.
		/// </summary>
		[TestMethod]
		public async Task TestIdDelete()
		{

			using (TestDbContext writeCtx = new TestDbContext())
			{

				WriteTestRepository writeRepo = new WriteTestRepository(writeCtx);

				//var notColor = new Color { Id = 42, Name = "Brown 50", Hex = "#efebe9" };
				try 
				{
					writeRepo.Delete<Color>(42);
					int deleted = await writeRepo.SaveAsync();
					Assert.Fail("Delete (id) did not throw exception for non-exist");
				}
				catch (ArgumentNullException) {} // Expected

			}

			// Create and delete
			using (TestDbContext writeCtx = new TestDbContext())
			{

				WriteTestRepository writeRepo = new WriteTestRepository(writeCtx);

				var newColor = new Color { Id = 52, Name = "Deep Purple 50", Hex = "#ede7f6" };
				writeRepo.Create(newColor, "Flagscript Unit Test");
				writeRepo.Save();
				var readTest = ReadTestRepo.GetOne<Color>(c => c.Id == 52);
				Assert.IsNotNull(readTest, "Can't proceed - color not created");
				ReadDbContext.Entry(readTest).State = EntityState.Detached;

				// Test marking.
				writeRepo.Delete<Color>(52);
				readTest = ReadTestRepo.GetOne<Color>(c => c.Id == 52);
				Assert.IsNotNull(readTest, "Deleted when should have marked.");
				ReadDbContext.Entry(readTest).State = EntityState.Detached;

				writeRepo.Save();
				readTest = ReadTestRepo.GetFirst<Color>(c => c.Id == 52);
				Assert.IsNull(readTest, "Did not delete");

			}


		}

		/// <summary>
		/// Tests the delete method.
		/// </summary>
		[TestMethod]
		public void TestDelete()
		{

			// Test doesn't exist
			using (TestDbContext writeCtx = new TestDbContext())
			{

				WriteTestRepository writeRepo = new WriteTestRepository(writeCtx);

				try 
				{
					var notColor = new Color { Id = 42, Name = "Brown 50", Hex = "#efebe9" };
					writeRepo.Delete(notColor);
					int deleted = writeRepo.Save();
					Assert.Fail("Delete (entity) did not throw exception for non-exist");
				}
				catch (DbUpdateConcurrencyException) {}

			}

			// Create and Delete
			using (TestDbContext writeCtx = new TestDbContext())
			{

				WriteTestRepository writeRepo = new WriteTestRepository(writeCtx);

				var newColor = new Color { Id = 42, Name = "Brown 50", Hex = "#efebe9" };
				writeRepo.Create(newColor, "Flagscript Unit Test");
				writeRepo.Save();
				var readTest = ReadTestRepo.GetOne<Color>(c => c.Id == 42);
				Assert.IsNotNull(readTest, "Can't proceed - color not created");
				ReadDbContext.Entry(readTest).State = EntityState.Detached;
				
				// Test marking.
				writeRepo.Delete(newColor);
				readTest = ReadTestRepo.GetOne<Color>(c => c.Id == 42);
				Assert.IsNotNull(readTest, "Deleted when should have marked.");
				ReadDbContext.Entry(readTest).State = EntityState.Detached;

				writeRepo.Save();
				readTest = ReadTestRepo.GetFirst<Color>(c => c.Id == 42);
				Assert.IsNull(readTest, "Did not delete");

			}

		}

		#endregion

	}

}
