using System.Collections.Generic;

namespace TennisExplorer.Test.TestData
{
	public static class FavoriteDataCreator
	{
		public static ICollection<TennisExplorer.Entity.Favorite> Data = new List<TennisExplorer.Entity.Favorite>();

		public async static void CreateData(TennisExplorer.Entity.TennisMatchSpyDbContext context)
		{
			Data.Add(new TennisExplorer.Entity.Favorite
			{
				Name = "Dimitrov"
			});

			Data.Add(new TennisExplorer.Entity.Favorite
			{
				Name = "Kerber"
			});

			await context.Favorites.AddRangeAsync(Data);
		}
	}
}
