using HtmlAgilityPack;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TennisExplorer.Services
{
	public interface IHtmlDownloader
	{
		Task<HtmlDocument> DownloadWebSiteContentAsync();
	}

	public class HtmlDownloader : IHtmlDownloader
	{
		public async Task<HtmlDocument> DownloadWebSiteContentAsync()
		{
			using (HttpClientHandler handler = new HttpClientHandler())
			{
				handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
				using (HttpClient client = new HttpClient(handler, false))
				{
					var source = await client.GetStringAsync("http://livetv.sx/dex/allupcomingsports/4");
					var htmlDocument = new HtmlDocument();
					htmlDocument.LoadHtml(source);

					return htmlDocument;
				}
			}
		}
	}
}
