using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using TennisExplorer.Test.Infrastructure;

namespace TennisExplorer.Test.IntegrationTest
{
	[TestClass]
	public class BaseIntegrationTest
	{
		protected TennisExplorer.Entity.TennisMatchSpyDbContext Context;
		private Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction;

		[AssemblyInitialize]
		public static void GlobalTestInitialize(TestContext testContext)
		{
			TestAppDependencySetup.SetupTestAppDependencies();
			InitializeDatabase();
		}

		[TestInitialize]
		public void Initialize()
		{
			// prepares a transaction for each database call
			// this call will be cancelled automatically during "dispose" so that no changes will be made to the database to influence other tests
			Context = TennisExplorer.Infrastructure.AppDependencySetup.Resolve<TennisExplorer.Entity.TennisMatchSpyDbContext>();
			var id = Context.GetHashCode();
			transaction = Context.Database.BeginTransaction();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			CleanupDbContextChanges();
			if (transaction != null)
			{
				transaction.Rollback();
				transaction.Dispose();
			}
		}

		private static void InitializeDatabase()
		{
			var dbInitializer = TennisExplorer.Infrastructure.AppDependencySetup.Resolve<DatabaseInitializer>();
			Task.Run(dbInitializer.InitializeAsync).Wait();
		}

		private void CleanupDbContextChanges()
		{
			foreach (var entry in Context.ChangeTracker.Entries().ToList())
			{
				switch (entry.State)
				{
					case Microsoft.EntityFrameworkCore.EntityState.Modified:
					case Microsoft.EntityFrameworkCore.EntityState.Deleted:
						entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified; //Revert changes made to deleted entity.
						entry.State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
						break;
					case Microsoft.EntityFrameworkCore.EntityState.Added:
						entry.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
						break;
				}
			}
		}
	}
}
