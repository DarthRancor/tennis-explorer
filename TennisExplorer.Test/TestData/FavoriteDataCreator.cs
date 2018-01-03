using System.Collections.Generic;

namespace TennisExplorer.Test.TestData
{
	public static class FavoriteDataCreator
	{
		public static ICollection<Entity.Favorite> Data = new List<Entity.Favorite>();

		static FavoriteDataCreator()
		{
			CreateData();
		}

		public static void CreateData()
		{
			Data.Add(new Entity.Favorite
			{
				Name = "Dimitrov"
			});

			Data.Add(new Entity.Favorite
			{
				Name = "Kerber"
			});
		}
	}
}
