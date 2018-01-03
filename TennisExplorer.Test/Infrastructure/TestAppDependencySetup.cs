using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using TennisExplorer.Infrastructure;
using TennisExplorer.Services;
using TennisExplorer.Test.Infrastructure;

namespace TennisExplorer.Test
{
	public static class TestAppDependencySetup
	{
		public static void SetupTestAppDependencies()
		{
			IServiceCollection services = new ServiceCollection();

			var config = new ApplicationConfiguration()
			{
				AppBasePath = GetDatabasePath(),
				DatabaseName = "TennisMatchSpyTest.db"
			};
			services.AddSingleton(config);

			//services.AddSingleton<IHtmlDownloader, LocalHtmlDownloader>();
			services.AddSingleton<DatabaseInitializer>();

			AppDependencySetup.ConfigureDependencies(services);
		}

		private static string GetDatabasePath()
		{
			string assemblyPath = Assembly.GetExecutingAssembly().Location;
			DirectoryInfo basePathInfo = new FileInfo(assemblyPath).Directory;

			return basePathInfo.FullName;
		}
	}
}
