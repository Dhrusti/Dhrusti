using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using WebScrapPOC.Helpers.CommonModels;

namespace WebScrapPOC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoController : ControllerBase
    {
        static ScrapingBrowser scrapingBrowser = new ScrapingBrowser();

        public CryptoController()
        {
        }

        [HttpGet("GetCryptoDetailsAsync")]
        public async Task<CommonResponse> GetCryptoDetailsAsync(string? screenURL = null)
        {
            if (screenURL == null) { screenURL = "https://www.coingecko.com/"; }
            CommonResponse response = new CommonResponse();
            try
            {
                var data = await GetModelListAsync(screenURL);
                if (data != null && data.Count > 0)
                {
                    response.Status = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Message = $"{data.Count} data found successfully!";
                    response.Data = data;
                }
                else
                {
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Message = "Can not find data!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        private static async Task<List<CryptoDetail>> GetModelListAsync(string screenURL)
        {
            var list = new List<CryptoDetail>();
            var links = (await GetHTMLAsync(screenURL)).CssSelect("tr");
            foreach (var item in links)
            {
                string Image = item.CssSelect("img").FirstOrDefault()?.Attributes["src"].Value ?? string.Empty;
                var Name = item.CssSelect("a span").FirstOrDefault()?.InnerHtml.Trim() ?? string.Empty;
                var ShortCode = item.CssSelect("a span.d-lg-inline").FirstOrDefault()?.InnerHtml.Trim() ?? string.Empty;
                var Price = item.CssSelect("span.no-wrap").FirstOrDefault()?.InnerHtml.Trim() ?? string.Empty;

                if (Name != string.Empty && ShortCode != string.Empty)
                    list.Add(new CryptoDetail { Image = Image, Name = Name, ShortCode = ShortCode, Price = Price });
            }
            return list;
        }

        private static async Task<HtmlNode> GetHTMLAsync(string screenURL)
        {
            scrapingBrowser.IgnoreCookies = true;
            scrapingBrowser.Timeout = TimeSpan.FromMinutes(15);
            WebPage webPage = await scrapingBrowser.NavigateToPageAsync(new Uri(screenURL));

            return webPage.Html;
        }

    }
}