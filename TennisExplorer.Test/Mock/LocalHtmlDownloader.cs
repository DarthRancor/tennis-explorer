using HtmlAgilityPack;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TennisExplorer.Service;

namespace TennisExplorer.Test.Infrastructure
{
	public class LocalHtmlDownloader : IHtmlDownloader
	{
		public Task<HtmlDocument> DownloadWebSiteContentAsync()
		{
			var localFileBytes = File.ReadAllBytes("Mock\\TennisMatchSite.html");
			string source = Encoding.GetEncoding("utf-8").GetString(localFileBytes, 0, localFileBytes.Length - 1);
			source = WebUtility.HtmlDecode(source);
			HtmlDocument html = new HtmlDocument();
			html.LoadHtml(source);

			return Task.FromResult(html);
		}
	}
}
