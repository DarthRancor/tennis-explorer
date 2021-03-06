﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
			// create a new entry that will be deleted
			var favoriteToDelete = new Favorite { Name = "Becker" };
			await _favoriteService.SaveFavorite(favoriteToDelete);

			await _favoriteService.DeleteFavorite(favoriteToDelete.Id);

			using (var database = GetDatabase())
			{
				var favoriteExists = database.GetCollection<Favorite>().Exists(f => f.Name == favoriteToDelete.Name);
				Assert.IsFalse(favoriteExists);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public async Task DeleteFavorite_ShouldThrowForNotExisting()
		{
			await _favoriteService.DeleteFavorite(Guid.NewGuid());
		}

		[TestMethod]
		public async Task SaveFavorite_ShouldCreateNewForNotExisting()
		{
			var newFavorite = new Favorite
			{
				Name = "A. Zverev"
			};

			await _favoriteService.SaveFavorite(newFavorite);

			using (var database = GetDatabase())
			{

				Assert.IsTrue(newFavorite.Id != Guid.Empty);
				var favoriteExists = database.GetCollection<Favorite>().Exists(f => f.Name == newFavorite.Name);
				Assert.IsTrue(favoriteExists);

				// cleanup
				await _favoriteService.DeleteFavorite(newFavorite.Id);
			}
		}

		//[TestMethod]
		//[ExpectedException(typeof(ServiceException))]
		//public async Task CreateFavorite_ShouldThrowForInvalidNewFavorite()
		//{
		//	var newFavorite = new Favorite
		//	{
		//		Name = null
		//	};

		//	await _favoriteService.CreateFavorite(newFavorite);
		//}

		//[TestMethod]
		//[ExpectedException(typeof(ServiceException))]
		//public async Task CreateFavorite_ShouldThrowForNotUniqueName()
		//{
		//	// take a name that already exists
		//	var favoriteToCreate = new Favorite
		//	{
		//		Name = FavoriteDataCreator.Data.ElementAt(0).Name
		//	};

		//	await _favoriteService.CreateFavorite(favoriteToCreate);
		//}

		[TestMethod]
		public async Task SaveFavorite_ShouldUpdateExisting()
		{
			var favoriteToChange = FavoriteDataCreator.Data.Single(f => f.Name == "Kerber");
			var originalName = favoriteToChange.Name;
			favoriteToChange.Name = "Kerber 2";

			await _favoriteService.SaveFavorite(favoriteToChange);

			using (var database = GetDatabase())
			{

				var updatedFavorite = database.GetCollection<Favorite>().FindById(favoriteToChange.Id);
				Assert.AreEqual(favoriteToChange.Name, updatedFavorite.Name);
			}

			// Undo changes
			favoriteToChange.Name = originalName;
			await _favoriteService.SaveFavorite(favoriteToChange);
		}


	}
}
