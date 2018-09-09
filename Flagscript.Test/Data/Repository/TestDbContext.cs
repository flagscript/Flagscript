using System;

using Microsoft.EntityFrameworkCore;

using Flagscript.Data.Entity;
using Microsoft.Data.Sqlite;

namespace Flagscript.Test.Data.Repository
{

	/// <summary>
	/// Test DbContext for testing package Flagscript.Data.Repository.
	/// </summary>
	public class TestDbContext : DbContext
	{

		#region Const / Static Fields

		/// <summary>
		/// Lock for in memory database setup.
		/// </summary>
		private static readonly object _setupLock = new object();

		/// <summary>
		/// The test context options.
		/// </summary>
		private static readonly DbContextOptions<TestDbContext> _testContextOptions
			= CreateDbContextOptions<TestDbContext>();

		#endregion

		#region Entity Properties

		/// <summary>
		/// Gets or sets the colors.
		/// </summary>
		/// <value>The colors.</value>
		public virtual DbSet<Color> Colors { get; set; }

		/// <summary>
		/// Gets or sets the fruits.
		/// </summary>
		/// <value>The fruits.</value>
		public virtual DbSet<Fruit> Fruits { get; set; }

		/// <summary>
		/// Gets or sets the nothings.
		/// </summary>
		/// <value>The nothings.</value>
		public virtual DbSet<NoData> Nothings { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TestDbContext"/> class using
		/// the in memory SqlLite test provider.
		/// </summary>
		public TestDbContext() : base(_testContextOptions)
		{
		}

		#endregion

		#region Static Setup Helpers

		/// <summary>
		/// Ensures the test DataStore is created and seeded with test data.
		/// </summary>
		public static void SetupDataStore()
		{
			lock(_setupLock)
			{
				using (TestDbContext testContext = new TestDbContext())
				{
					testContext.Database.EnsureCreated();
				}
			}
		}

		/// <summary>
		/// Creates the <see cref="DbContextOptions"/> for an in memory test 
		/// SqlLite databast.
		/// </summary>
		/// <returns>The DbContext options.</returns>
		/// <typeparam name="T">DbContext Type.</typeparam>
		private static DbContextOptions<T> CreateDbContextOptions<T>()
			where T : DbContext
		{

			// Create Connection to SqlLite in memory database. 
			var connectionString = new SqliteConnectionStringBuilder { DataSource = ":memory:" }
				.ToString();
			var connection = new SqliteConnection(connectionString);
			connection.Open();

			// Build the options
			var optionsBuilder = new DbContextOptionsBuilder<T>();
			optionsBuilder
				.UseLazyLoadingProxies()
				.EnableSensitiveDataLogging()
				.UseSqlite(connection);

			return optionsBuilder.Options;

		}

		#endregion

		#region Overrides

		/// <summary>
		/// Configure the model configured by convention.
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			var redColor = new Color
			{
				Id = 1,
				Name = "Red",
				Hex = "#ff0000",
				CreatedBy = "Flagscript Unit Test"
			};

			var yellowColor = new Color
			{
				Id = 2,
				Name = "Yellow",
				Hex = "#ffff00",
				CreatedBy = "Flagscript Unit Test"
			};

			modelBuilder.Entity<Color>()
				.HasData(
					redColor,
					yellowColor,
					new Color { Id = 3, Name = "White", Hex = "#ffffff", CreatedBy = "Flagscript Unit Test" },
					new Color { Id = 4, Name = "Black", Hex = "#000000", CreatedBy = "Flagscript Unit Test" },
					new Color { Id = 5, Name = "Grey", Hex = "#808080", CreatedBy = "Flagscript Unit Test" },
					new Color { Id = 6, Name = "Lime", Hex = "#00ff00", CreatedBy = "Flagscript Unit Test" },
					new Color { Id = 7, Name = "Green", Hex = "#008000", CreatedBy = "Flagscript Unit Test" },
					new Color { Id = 8, Name = "Aqua", Hex = "#00ffff", CreatedBy = "Flagscript Unit Test" },
					new Color { Id = 9, Name = "Teal", Hex = "#008080", CreatedBy = "Flagscript Unit Test" },
					new Color { Id = 10, Name = "Fushia", Hex = "#ff00ff", CreatedBy = "Flagscript Unit Test" }
				);

			modelBuilder.Entity<Fruit>()
				.HasOne(f => f.Color)
				.WithMany()
				.HasForeignKey(f => f.ColorId);

			modelBuilder.Entity<Fruit>()
				.HasData(
					new Fruit { Id = 1, Name = "Apple", ColorId = redColor.Id },
					new Fruit { Id = 2, Name = "Banana", ColorId = yellowColor.Id }
				);
		}

		#endregion

	}

	/// <summary>
	/// Test Color Entity Class
	/// </summary>
	public class Color : Entity<int>
	{

		/// <summary>
		/// Gets or sets the hex.
		/// </summary>
		/// <value>The hex.</value>
		public string Hex { get; set; }

	}

	/// <summary>
	/// Test Fruit Entity Class.
	/// </summary>
	public class Fruit : Entity<int>
	{

		/// <summary>
		/// <see cref="Color"/> foreign key.
		/// </summary>
		/// <value><see cref="Color"/> foreign key.</value>
		public int ColorId { get; set; }

		/// <summary>
		/// Gets or sets the color of the fruit.
		/// </summary>
		/// <value>The color of the fruit.</value>
		public virtual Color Color { get; set; }

	}

	/// <summary>
	/// Random entity for no data.
	/// </summary>
	public class NoData : Entity<Guid>
	{	
	}


}
