using Microsoft.EntityFrameworkCore;
using TennisExplorer.Infrastructure;

namespace TennisExplorer.Entity
{
	public class TennisMatchSpyDbContext : DbContext
	{
		public DbSet<Favorite> Favorites { get; set; }

		private readonly ApplicationConfiguration _applicationConfiguration;

		public TennisMatchSpyDbContext(DbContextOptions<TennisMatchSpyDbContext> options, ApplicationConfiguration applicationConfiguration) : base(options)
		{
			_applicationConfiguration = applicationConfiguration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var path = System.IO.Path.Combine(_applicationConfiguration.AppBasePath, _applicationConfiguration.DatabaseName);
			optionsBuilder.UseSqlite($"Filename={path};");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Favorite>()
				.HasIndex(d => d.Name)
				.IsUnique();
		}
	}
}
