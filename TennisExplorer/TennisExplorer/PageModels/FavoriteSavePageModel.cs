using System;
using System.Collections.ObjectModel;
using System.Linq;
using TennisExplorer.Services;
using Xamarin.Forms;

namespace TennisExplorer.PageModels
{
	public class FavoriteSavePageModel : BasePageModel
	{
		private readonly FavoriteService _favoriteService;

		public Entity.Favorite Favorite { get; set; } = new Entity.Favorite();
		
		public FavoriteSavePageModel(FavoriteService favoriteService)
		{
			_favoriteService = favoriteService;
		}

		public override void Init(object initData)
		{
			if (initData != null)
			{
				Favorite = initData as Entity.Favorite;
			}
		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);
			
		}

		public Command SaveFavoriteCommand
		{
			get
			{
				return new Command(async () => {
					await _favoriteService.SaveFavorite(Favorite);
					await CoreMethods.PopPageModel(modal: true);
				});
			}
		}

		public Command CancelCommand
		{
			get
			{
				return new Command(async () => {
					await CoreMethods.PopPageModel(modal: true);
				});
			}
		}
	}
}
