using System.Threading.Tasks;
using TennisExplorer.Infrastructure;

namespace TennisExplorer.Test.Infrastructure
{
	public class DatabaseInitializer
	{
		private readonly Microsoft.EntityFrameworkCore.DbContextOptions<TennisExplorer.Entity.TennisMatchSpyDbContext> _options;
		private readonly ApplicationConfiguration _applicationConfiguration;

		public DatabaseInitializer(Microsoft.EntityFrameworkCore.DbContextOptions<TennisExplorer.Entity.TennisMatchSpyDbContext> options, ApplicationConfiguration applicationConfiguration)
		{
			_options = options;
			_applicationConfiguration = applicationConfiguration;
		}

		public async Task InitializeAsync()
		{
			using (var context = new TennisExplorer.Entity.TennisMatchSpyDbContext(_options, _applicationConfiguration))
			{
				var id = context.GetHashCode();
				await RecreateDatabaseAsync(context);
				await SeedDataAsync(context);
			}
		}

		private async Task RecreateDatabaseAsync(TennisExplorer.Entity.TennisMatchSpyDbContext context)
		{
			await context.Database.EnsureDeletedAsync();
			await context.Database.EnsureCreatedAsync();
		}

		protected async Task SeedDataAsync(TennisExplorer.Entity.TennisMatchSpyDbContext context)
		{
			TestData.FavoriteDataCreator.CreateData(context);
			await context.SaveChangesAsync();
		}
	}
}
