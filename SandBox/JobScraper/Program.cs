using AngleSharp;
using AngleSharp.Html.Dom;
using System.Text;

namespace JobScraper
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var url = @"https://www.1001recepti.com/recipes/";

            var config = Configuration.Default.WithDefaultLoader();

            var doc = await BrowsingContext.New(config).OpenAsync(url);

            var categoryName = doc.QuerySelectorAll("#cont_recipe_browse_big > div > a").OfType<IHtmlAnchorElement>().ToList();

        }
    }
}