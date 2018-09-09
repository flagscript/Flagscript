using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flagscript.Test.Data.Repository
{

	/// <summary>
	/// Entity framework read only repository tests.
	/// </summary>
	[TestClass]
	public class EntityFrameworkReadOnlyRepositoryTest
	{

		#region Static Properties

		/// <summary>
		/// Repository to use for testing.
		/// </summary>
		private static ReadOnlyTestRepository TestRepo { get; set; }

		/// <summary>
		/// DbContext to use for testing.
		/// </summary>
		private static TestDbContext DbContext { get; set; }

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
			DbContext = new TestDbContext();
			TestRepo = new ReadOnlyTestRepository(DbContext);
		}

		/// <summary>
		/// Cleans up objects used for all tests.
		/// </summary>
		[ClassCleanup]
		public static void TestClassTeardown()
		{
			TestRepo = null;
			DbContext.Dispose();
			DbContext = null;
		}

		#endregion

		#region Test Cases

		/// <summary>
		/// Test sync read only repo getall.
		/// </summary>
		[TestMethod]
		public void SyncGetAllTest()
		{
		
			var getAllColorResult = TestRepo.GetAll<Color>();
			Assert.AreEqual(10, getAllColorResult.Count(), "Sync GetAll Basic Wrong Count");
			getAllColorResult = TestRepo.GetAll<Color>(
				orderBy: q => q.OrderByDescending(c => c.Name),
				skip: 3,
				take: 2
			);
			Assert.AreEqual(2, getAllColorResult.Count(), "Sync GetAll Take Wrong Count.");
			Assert.IsTrue(
				getAllColorResult.Any(c => c.Name == "Lime") && getAllColorResult.Any(c => c.Name == "Red"),
				"Sync GetAll Basic Skip Wrong"
			);
			Assert.IsTrue(getAllColorResult.First().Name == "Red", "Sync GetAll Sort Wrong");

			var getAllFruitResult = TestRepo.GetAll<Fruit>(includeProperties: "Color");
			Assert.AreEqual(2, getAllFruitResult.Count(), "Sync GetAll Wrong Fruit Count.");
			Assert.IsNotNull(getAllFruitResult.First().Color, "Sync GetAll include did not work.");

		}

		/// <summary>
		/// Test async read only repo getall.
		/// </summary>
		[TestMethod]
		public async Task AsyncGetAllTest()
		{

			var getAllColorResult = await TestRepo.GetAllAsync<Color>();
			Assert.AreEqual(10, getAllColorResult.Count(), "Sync GetAll Basic Wrong Count");
			getAllColorResult = await TestRepo.GetAllAsync<Color>(
				orderBy: q => q.OrderByDescending(c => c.Name),
				skip: 3,
				take: 2
			);
			Assert.AreEqual(2, getAllColorResult.Count(), "Sync GetAll Take Wrong Count.");
			Assert.IsTrue(
				getAllColorResult.Any(c => c.Name == "Lime") && getAllColorResult.Any(c => c.Name == "Red"),
				"Sync GetAll Basic Skip Wrong"
			);
			Assert.IsTrue(getAllColorResult.First().Name == "Red", "Sync GetAll Sort Wrong");

			var getAllFruitResult = await TestRepo.GetAllAsync<Fruit>(includeProperties: "Color");
			Assert.AreEqual(2, getAllFruitResult.Count(), "Sync GetAll Wrong Fruit Count.");
			Assert.IsNotNull(getAllFruitResult.First().Color, "Sync GetAll include did not work.");

		}

		/// <summary>
		/// Test sync read only repo get.
		/// </summary>
		[TestMethod]
		public void SyncGetTest()
		{

			var getColorResult = TestRepo.Get<Color>();
			Assert.AreEqual(10, getColorResult.Count(), "Sync Get Null Filter Basic Wrong Count");
			getColorResult = TestRepo.Get<Color>(c => c.Hex.Contains("ff"));
			Assert.AreEqual(6, getColorResult.Count(), "Sync Get Filter Basic Wrong Count");
			getColorResult = TestRepo.Get<Color>(
				filter: c => c.Hex.Contains("ff"),
				orderBy: q => q.OrderByDescending(c => c.Name),
				skip: 1,
				take: 2
			);
			Assert.AreEqual(2, getColorResult.Count(), "Sync Get Take Wrong Count.");
			Assert.IsTrue(
				getColorResult.Any(c => c.Name == "Red") && getColorResult.Any(c => c.Name == "White"),
				"Sync Get Basic Skip Wrong"
			);
			Assert.IsTrue(getColorResult.First().Name == "White", "Sync Get Sort Wrong");

			var getFruitResult = TestRepo.Get<Fruit>(filter: f => f.Name == "Banana", includeProperties: "Color");
			Assert.AreEqual(1, getFruitResult.Count(), "Sync Get Wrong Fruit Count.");
			Assert.IsNotNull(getFruitResult.First().Color, "Sync Get include did not work.");

		}

		/// <summary>
		/// Test async read only repo get.
		/// </summary>
		[TestMethod]
		public async Task AsyncGetTest()
		{

			var getColorResult = await TestRepo.GetAsync<Color>();
			Assert.AreEqual(10, getColorResult.Count(), "Sync Get Null Filter Basic Wrong Count");
			getColorResult = await TestRepo.GetAsync<Color>(c => c.Hex.Contains("ff"));
			Assert.AreEqual(6, getColorResult.Count(), "Sync Get Filter Basic Wrong Count");
			getColorResult = await TestRepo.GetAsync<Color>(
				filter: c => c.Hex.Contains("ff"),
				orderBy: q => q.OrderByDescending(c => c.Name),
				skip: 1,
				take: 2
			);
			Assert.AreEqual(2, getColorResult.Count(), "Sync Get Take Wrong Count.");
			Assert.IsTrue(
				getColorResult.Any(c => c.Name == "Red") && getColorResult.Any(c => c.Name == "White"),
				"Sync Get Basic Skip Wrong"
			);
			Assert.IsTrue(getColorResult.First().Name == "White", "Sync Get Sort Wrong");

			var getFruitResult = await TestRepo.GetAsync<Fruit>(filter: f => f.Name == "Banana", includeProperties: "Color");
			Assert.AreEqual(1, getFruitResult.Count(), "Sync Get Wrong Fruit Count.");
			Assert.IsNotNull(getFruitResult.First().Color, "Sync Get include did not work.");

		}

		/// <summary>
		/// Test sync read only repo getone.
		/// </summary>
		[TestMethod]
		public void SyncGetOneTest()
		{

			try 
			{
				TestRepo.GetOne<Color>();
				Assert.Fail("Sync GetOne No Filter didn't exception");
			}
			catch (InvalidOperationException) {} // Expected
			try 
			{
				TestRepo.GetOne<Color>(c => c.Hex.Contains("ff"));
				Assert.Fail("Sync GetOne Wide Filter didn't exception");
			}
			catch (InvalidOperationException) {} // Expected

			var getOneFruitResult = TestRepo.GetOne<Fruit>(filter: f => f.Name == "Banana", includeProperties: "Color");
			Assert.IsNotNull(getOneFruitResult, "Sync GetOne valid returned null.");
			Assert.IsNotNull(getOneFruitResult.Color, "Sync GetOne valid include did not work.");

		}

		/// <summary>
		/// Test async read only repo getone.
		/// </summary>
		[TestMethod]
		public async Task AsyncGetOneTest()
		{

			try
			{
				await TestRepo.GetOneAsync<Color>();
				Assert.Fail("Async GetOne No Filter didn't exception");
			}
			catch (InvalidOperationException) { } // Expected
			try
			{
				await TestRepo.GetOneAsync<Color>(c => c.Hex.Contains("ff"));
				Assert.Fail("Async GetOne Wide Filter didn't exception");
			}
			catch (InvalidOperationException) { } // Expected

			var getOneFruitResult = await TestRepo.GetOneAsync<Fruit>(filter: f => f.Name == "Banana", includeProperties: "Color");
			Assert.IsNotNull(getOneFruitResult, "Async GetOne valid returned null.");
			Assert.IsNotNull(getOneFruitResult.Color, "Async GetOne valid include did not work.");

		}

		/// <summary>
		/// Test sync read only repo getfirst.
		/// </summary>
		[TestMethod]
		public void SyncGetFirstTest()
		{

			var getFirstResult = TestRepo.GetFirst<Color>();
			Assert.IsNotNull(getFirstResult, "Sync GetFirst no filter returned null");
			getFirstResult = TestRepo.GetFirst<Color>(filter: c => c.Hex.Contains("ff"));
			Assert.IsTrue(getFirstResult.Hex.Contains("ff"), "Sync GetFirst filter bad result");
			var getFirstReverseResult = TestRepo.GetFirst<Color>(
				filter: c => c.Hex.Contains("ff"),
				orderBy: q => q.OrderByDescending(c => c.Id)
			);
			Assert.AreNotEqual(getFirstResult.Name, getFirstReverseResult.Name,
				"Sync GetFirst OrderBy not working");

			var getFirstFruitResult = TestRepo.GetFirst<Fruit>();
			Assert.IsNotNull(getFirstFruitResult.Color, "Sync GetFirst include did not work.");

		}

		/// <summary>
		/// Test async read only repo getfirst.
		/// </summary>
		[TestMethod]
		public async Task AsyncGetFirstTest()
		{

			var getFirstResult = await TestRepo.GetFirstAsync<Color>();
			Assert.IsNotNull(getFirstResult, "Async GetFirst no filter returned null");
			getFirstResult = await TestRepo.GetFirstAsync<Color>(filter: c => c.Hex.Contains("ff"));
			Assert.IsTrue(getFirstResult.Hex.Contains("ff"), "Async GetFirst filter bad result");
			var getFirstReverseResult = TestRepo.GetFirst<Color>(
				filter: c => c.Hex.Contains("ff"),
				orderBy: q => q.OrderByDescending(c => c.Id)
			);
			Assert.AreNotEqual(getFirstResult.Name, getFirstReverseResult.Name,
				"Async GetFirst OrderBy not working");

			var getFirstFruitResult = await TestRepo.GetFirstAsync<Fruit>();
			Assert.IsNotNull(getFirstFruitResult.Color, "Async GetFirst include did not work.");

		}

		/// <summary>
		/// Test sync read only repo getbyid.
		/// </summary>
		[TestMethod]
		public void SyncGetByIdTest()
		{

			var getByIdResult = TestRepo.GetById<Color>(14);
			Assert.IsNull(getByIdResult, "Sync GetById invalid ID returned element");
			getByIdResult = TestRepo.GetById<Color>(1);
			Assert.AreEqual("Red", getByIdResult.Name, "Sync GetById returned wrong element");

		}

		/// <summary>
		/// Test async read only repo getbyid.
		/// </summary>
		[TestMethod]
		public async Task AsyncGetByIdTest()
		{

			var getByIdResult = await TestRepo.GetByIdAsync<Color>(14);
			Assert.IsNull(getByIdResult, "Async GetById invalid ID returned element");
			getByIdResult = await TestRepo.GetByIdAsync<Color>(1);
			Assert.AreEqual("Red", getByIdResult.Name, "Async GetById returned wrong element");

		}

		/// <summary>
		/// Test sync read only repo getcount.
		/// </summary>
		[TestMethod]
		public void SyncGetCountTest()
		{

			var getCountResult = TestRepo.GetCount<Color>();
			Assert.AreEqual(10, getCountResult, "Sync GetCount no filter returned wrong count");
			getCountResult = TestRepo.GetCount<Color>(filter: c => c.Hex.Contains("ff"));
			Assert.AreEqual(6, getCountResult, "Sync GetCount filter returned wrong count");

		}

		/// <summary>
		/// Test async read only repo getcount.
		/// </summary>
		[TestMethod]
		public async Task AsyncGetCountTest()
		{

			var getCountResult = await TestRepo.GetCountAsync<Color>();
			Assert.AreEqual(10, getCountResult, "Async GetCount no filter returned wrong count");
			getCountResult = await TestRepo.GetCountAsync<Color>(filter: c => c.Hex.Contains("ff"));
			Assert.AreEqual(6, getCountResult, "Async GetCount filter returned wrong count");

		}

		/// <summary>
		/// Test sync read only repo exists.
		/// </summary>
		[TestMethod]
		public void SyncExistsTest()
		{

			var existsResult = TestRepo.Exists<Color>();
			Assert.IsTrue(existsResult, "Sync Exists no filter did not return true");
			existsResult = TestRepo.Exists<NoData>();
			Assert.IsFalse(existsResult, "Sync Exists no filter with nothing did not return false");
			existsResult = TestRepo.Exists<Color>(c => c.Hex.Contains("ff"));
			Assert.IsTrue(existsResult, "Sync Exists good filter did not return true");
			existsResult = TestRepo.Exists<Color>(c => c.Hex.Contains("xx"));
			Assert.IsFalse(existsResult, "Sync Exists bad filter did not return true");

		}

		/// <summary>
		/// Test async read only repo exists.
		/// </summary>
		[TestMethod]
		public async Task AsyncExistsTest()
		{

			var existsResult = await TestRepo.ExistsAsync<Color>();
			Assert.IsTrue(existsResult, "Sync Exists no filter did not return true");
			existsResult = await TestRepo.ExistsAsync<NoData>();
			Assert.IsFalse(existsResult, "Sync Exists no filter with nothing did not return false");
			existsResult = await TestRepo.ExistsAsync<Color>(c => c.Hex.Contains("ff"));
			Assert.IsTrue(existsResult, "Sync Exists good filter did not return true");
			existsResult = await TestRepo.ExistsAsync<Color>(c => c.Hex.Contains("xx"));
			Assert.IsFalse(existsResult, "Sync Exists bad filter did not return true");

		}

		#endregion


	}

}
