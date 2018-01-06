using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TennisExplorer.Models;

namespace TennisExplorer.Services
{
	public class TennisMatchRetriever
	{
		private readonly IHtmlDownloader htmlDownloader;	

		public TennisMatchRetriever(IHtmlDownloader htmlDownloader)
		{
			this.htmlDownloader = htmlDownloader;
		}
	
		public async Task<List<TennisMatch>> GetTennisMatchesForDateAsync(DateTime date)
		{
			var html = await htmlDownloader.DownloadWebSiteContentAsync();
			var rows = GetMatchTableRowsForDate(html, date);
			var matchElements = GetMatchElements(rows).ToList();

			int id = 1;
			var matches = matchElements.Select(node => ParseMatchDetails(node, id++)).ToList();
			return matches;
		}

		private List<HtmlNode> GetMatchTableRowsForDate(HtmlDocument html, DateTime date)
		{
			var formattedDate = GetFormattedDateForMatches(date);
			var todaysMatchElement = GetNodeWithMatchesForDate(html, formattedDate);
			
			var result = new List<HtmlNode>();
			if (todaysMatchElement != null)
			{
				var matchesTableBody = todaysMatchElement.ParentNode.ParentNode.ParentNode;
				var allMatchesRows = matchesTableBody.ChildNodes.Where(n => n.Name == "tr").Skip(2).ToList();
				var todaysMatchesRows = allMatchesRows.TakeWhile(row => row.ChildNodes.First(n => n.Name == "td").GetAttributeValue("colspan", "") == "2");

				result = todaysMatchesRows.ToList();
			}

			return result;
		}

		private string GetFormattedDateForMatches(DateTime date)
		{
			// use the german format as the url is pointing to the german language
			var formattedDate = date.ToString("d MMMM, dddd", new CultureInfo("de-DE"));
			return formattedDate;
		}

		private HtmlNode GetNodeWithMatchesForDate(HtmlDocument document, string formattedDate)
		{
			var dateRowQuery = document.DocumentNode.Descendants("b").Where(d => d.InnerText == formattedDate).ToList();
			return dateRowQuery.FirstOrDefault();
		}

		private List<HtmlNode> GetMatchElements(List<HtmlNode> matchRows)
		{
			// get the "event descriptions" and only those that are concrete matches (they contain " &ndash; ") 
			var matchSpans = matchRows.SelectMany(row => row.Descendants("span")
																.Where(s => s.GetAttributeValue("class", "") == "evdesc")).ToList();

			var matchDescriptions = matchSpans.Where(span => IsMatch(span));
			return matchDescriptions.ToList();
		}

		private bool IsMatch(HtmlNode span)
		{
			var playersElement = GetPlayersForMatchElement(span).InnerHtml;
			return playersElement.Contains(" &ndash; ") || playersElement.Contains(" – ");
		}

		private HtmlNode GetPlayersForMatchElement(HtmlNode matchSpan)
		{
			// the players are within the sibling as an anker
			var playersElement = matchSpan.ParentNode.ChildNodes.Where(n => n.Name == "a").First();
			return playersElement;
		}

		private TennisMatch ParseMatchDetails(HtmlNode node, int id)
		{
			var tennisMatch = new TennisMatch();
			tennisMatch.Id = id;
			
			var timeAndTour = node.InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", "");
			// pattern is: <time>(<tour>)
			var splitted = timeAndTour.Split('(');

			var tour = splitted[1].Replace(")", "");
			tennisMatch.Time = splitted[0];
			tennisMatch.Tour = DecodeValue(tour);

			var playersElement = GetPlayersForMatchElement(node);
			var playersNames = playersElement.InnerHtml.Replace("<br>", " - ");
			tennisMatch.Players = DecodeValue(playersNames);
			
			return tennisMatch;
		}

		private string DecodeValue(string value)
		{
			return HtmlEntity.DeEntitize(value);
		}
	}
}
