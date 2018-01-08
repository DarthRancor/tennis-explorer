using System;
using System.Collections.ObjectModel;
using System.Linq;
using TennisExplorer.Services;
using Xamarin.Forms;

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

		public Command AddFavoriteCommand
		{
			get
			{
				return new Command(async () =>
				{
					await CoreMethods.PushPageModel<FavoriteSavePageModel>(null, modal: true);
				});
			}
		}

		public Command<Entity.Favorite> EditFavoriteCommand
		{
			get
			{
				return new Command<Entity.Favorite>(async (favorite) =>
				{
					await CoreMethods.PushPageModel<FavoriteSavePageModel>(favorite, modal: true);
				});
			}
		}

		public Command<Entity.Favorite> DeleteFavoriteCommand
		{
			get
			{
				return new Command<Entity.Favorite>(async (favorite) =>
				{
					var question = $"Are you sure you want to delete this player: {favorite.Name}?";
					var shouldDelete = await CoreMethods.DisplayAlert("Delete favorite player", question, "Yes", "No");
					if (shouldDelete)
					{
						await _favoriteService.DeleteFavorite(favorite.Id);
						BindFavorites();
					}
				});
			}
		}



	}
}
