using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TennisExplorer.Models;

namespace TennisExplorer.Services
{
	public interface ITennisMatchService
	{
		Task<List<TennisMatch>> GetTodaysTennisMatchesAsync();

		Task<List<TennisMatch>> GetTennisMatchesForDateAsync(DateTime date);
	}

	public class TennisMatchService : ITennisMatchService
	{
		private readonly TennisMatchRetriever tennisMatchRetriever;	

		public TennisMatchService(TennisMatchRetriever tennisMatchRetriever)
		{
			this.tennisMatchRetriever = tennisMatchRetriever;
		}
	
		public async Task<List<TennisMatch>> GetTodaysTennisMatchesAsync()
		{
			var matches = await GetTennisMatchesForDateAsync(DateTime.Today);
			return matches;
		}

		public async Task<List<TennisMatch>> GetTennisMatchesForDateAsync(DateTime date)
		{
			//var matches = await tennisMatchRetriever.GetTennisMatchesForDateAsync(date);
			await Task.Delay(3000);
			var matches = new List<Models.TennisMatch>
			{
				new Models.TennisMatch { Players = "Kerber - Hingis", Time = "13.07.2017 15:30", Tour = "ATP", IsFavorite = true},
				new Models.TennisMatch { Players = "Test3 - Test4", Time = "13.07.2017 15:45"},
				new Models.TennisMatch { Players = "Test3 - Test4", Time = "13.07.2017 15:45"}
			};

			return matches;
		}

		// save favorite matches to db?
	}
}
