using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using System;
using TennisExplorer.Services;
using TennisExplorer.Infrastructure;

namespace TennisExplorer.Test.UnitTest
{
	[TestClass]
	public class TennisMatchServiceTest : BaseUnitTest
	{
		private readonly ITennisMatchService _tennisMatchService;

		public TennisMatchServiceTest()
		{
			_tennisMatchService = AppDependencySetup.Resolve<ITennisMatchService>();
		}

		[TestMethod]
		public async Task GetTennisMatchesForDateAsync_ShouldFindSomeMatches()
		{
			var matches = await _tennisMatchService.GetTennisMatchesForDateAsync(new DateTime(2017, 5, 7));
			Assert.IsTrue(matches.Any());

			// check that some of the matched are found
			Assert.IsTrue(matches.Any(m => m.Players.Equals("Ana Konjuh – Kristina Mladenovic")));
			Assert.IsTrue(matches.Any(m => m.Players.Equals("Maria Sharapova – Mirjana Lucic-Baroni")));
			Assert.IsTrue(matches.Any(m => m.Players.Equals("Guido Pella – Alexander Zverev")));
		}

		[TestMethod]
		public async Task GetTennisMatchesForDateAsync_ShouldContainMatchesOnly()
		{
			var matches = await _tennisMatchService.GetTennisMatchesForDateAsync(new DateTime(2017, 5, 7));
			Assert.IsTrue(matches.All(m => m.Players.Contains(" – ")));
		}

		[TestMethod]
		public async Task GetTennisMatchesForDateAsync_ShouldContainDetails()
		{
			var matches = await _tennisMatchService.GetTennisMatchesForDateAsync(new DateTime(2017, 5, 7));
			var matchToCheck = matches.FirstOrDefault(m => m.Players.Contains("Kerber"));

			Assert.IsNotNull(matchToCheck);
			Assert.AreEqual("Angelique Kerber – Timea Babos", matchToCheck.Players);
			Assert.AreEqual("7 Mai um 11:10", matchToCheck.Time);
			Assert.AreEqual("(WTA Tour)", matchToCheck.Tour);
			Assert.AreEqual(5, matchToCheck.Id);
		}
	}
}
