using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisExplorer.Services;

namespace TennisExplorer.PageModels
{
	public class TodaysMatchesPageModel : BasePageModel
	{
		private readonly ITennisMatchService _tennisMatchService;
		public bool MatchesFound { get; set; } = true;
		public List<Models.TennisMatch> Matches { get; set; } = new List<Models.TennisMatch>();

		public TodaysMatchesPageModel(ITennisMatchService tennisMatchService)
		{
			_tennisMatchService = tennisMatchService;
		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);
			Task.Run(BindMatches);
		}

		private async Task BindMatches()
		{
			if (!Matches.Any())
			{
				IsBusy = true;
				try
				{
					Matches = await _tennisMatchService.GetTodaysTennisMatchesAsync();
					MatchesFound = Matches.Any();
				}
				finally
				{
					IsBusy = false;
				}
			}
		}
	}
}
