using System.IO;

namespace TennisExplorer.Infrastructure
{
	public class ApplicationConfiguration
	{
		public string AppBasePath { get; set; }

		public string DatabaseName { get; set; }

		public string DatabaseFullPath
		{
			get { return Path.Combine(AppBasePath, DatabaseName); }
		}
	}
}
