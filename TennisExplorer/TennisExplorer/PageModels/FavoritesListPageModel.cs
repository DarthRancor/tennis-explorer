using System;
using System.Collections.ObjectModel;
using System.Linq;
using TennisExplorer.Services;

namespace TennisExplorer.PageModels
{
	public class FavoritesListPageModel : BasePageModel
	{
		private readonly FavoriteService _favoriteService;

		public ObservableCollection<Entity.Favorite> Favorites { get; set; }
		public Entity.Favorite SelectedFavorite { get; set; }
		public bool FavoritesFound { get; set; } = true;

		public FavoritesListPageModel(FavoriteService favoriteService)
		{
			_favoriteService = favoriteService;
		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);
			BindFavorites();
		}

		private async void BindFavorites()
		{
			var allFavorites = await _favoriteService.GetAllFavorites();
			Favorites = new ObservableCollection<Entity.Favorite>(allFavorites);
			FavoritesFound = Favorites.Any();
		}
	}
}
