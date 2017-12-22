using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using TennisExplorer.Entity;
using TennisExplorer.Infrastructure;
using TennisExplorer.Services;
using TennisExplorer.Test.TestData;

namespace TennisExplorer.Test.IntegrationTest
{
	[TestClass]
	public class FavoriteServiceTest : BaseIntegrationTest
	{
		private readonly FavoriteService _favoriteService;

		public FavoriteServiceTest()
		{
			_favoriteService = AppDependencySetup.Resolve<FavoriteService>();
		}

		[TestMethod]
		public async Task GetFavorites_ShouldFindAll()
		{
			var foundFavorites = await _favoriteService.GetAllFavorites();

			Assert.AreEqual(FavoriteDataCreator.Data.ElementAt(0).Id, foundFavorites.ElementAt(0).Id); 
			Assert.AreEqual(FavoriteDataCreator.Data.ElementAt(0).Name, foundFavorites.ElementAt(0).Name);
			Assert.AreEqual(FavoriteDataCreator.Data.ElementAt(1).Id, foundFavorites.ElementAt(1).Id);
			Assert.AreEqual(FavoriteDataCreator.Data.ElementAt(1).Name, foundFavorites.ElementAt(1).Name);
		}

		[TestMethod]
		public async Task DeleteFavorite_ShouldRemoveExisting()
		{
			var favoriteToDelete = FavoriteDataCreator.Data.Single(f => f.Name == "Kerber");
			
			await _favoriteService.DeleteFavorite(favoriteToDelete.Id);

			var favoriteStillExists = Context.Favorites.Any(f => f.Name == favoriteToDelete.Name);
			Assert.IsFalse(favoriteStillExists);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public async Task DeleteFavorite_ShouldThrowForNotExisting()
		{
			await _favoriteService.DeleteFavorite(-1);
		}

		[TestMethod]
		public async Task CreateFavorite_ShouldCreateNewForNotExisting()
		{
			var newFavorite = new Favorite
			{
				Name = "Zverev"
			};

			await _favoriteService.CreateFavorite(newFavorite);

			Assert.IsTrue(newFavorite.Id > 0);
			Assert.IsTrue(Context.Favorites.Any(f => f.Name == newFavorite.Name));
		}

		[TestMethod]
		[ExpectedException(typeof(ServiceException))]
		public async Task CreateFavorite_ShouldThrowForInvalidNewFavorite()
		{
			var newFavorite = new Favorite
			{
				Name = null
			};

			await _favoriteService.CreateFavorite(newFavorite);
		}

		[TestMethod]
		[ExpectedException(typeof(ServiceException))]
		public async Task CreateFavorite_ShouldThrowForNotUniqueName()
		{
			// take a name that already exists
			var favoriteToCreate = new Favorite
			{
				Name = FavoriteDataCreator.Data.ElementAt(0).Name
			};

			await _favoriteService.CreateFavorite(favoriteToCreate);
		}

		[TestMethod]
		public async Task ChangeFavorite_ShouldUpdateExisting()
		{
			var favoriteToChange = FavoriteDataCreator.Data.Single(f => f.Name == "Kerber");
			favoriteToChange.Name = "Kerber 2";

			await _favoriteService.ChangeFavorite(favoriteToChange);

			var updatedFavorite = await Context.Favorites.FindAsync(favoriteToChange.Id);
			Assert.AreEqual(favoriteToChange.Name, updatedFavorite.Name);

			// Undo changes
			favoriteToChange.Name = "Kerber";
		}

		
	}
}
