using HtmlAgilityPack;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
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
					var byteMarkup = await client.GetByteArrayAsync("http://livetv.sx/dex/allupcomingsports/4");

					// get utf8
					var encodedMarkup = Encoding.UTF8.GetString(byteMarkup, 0, byteMarkup.Length);

					var htmlDocument = new HtmlDocument();
					htmlDocument.LoadHtml(encodedMarkup);

					return htmlDocument;
				}
			}
		}
	}
}
