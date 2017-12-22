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
			var matches = await tennisMatchRetriever.GetTennisMatchesForDateAsync(date);
			return matches;
		}

		// save favorite matches to db?
	}
}
