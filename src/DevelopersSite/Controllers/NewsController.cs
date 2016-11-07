using System.Threading.Tasks;
using DevelopersSite.Enums;
using DevelopersSite.Models;
using DevelopersSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevelopersSite.Controllers
{
    public class NewsController : Controller
    {
        readonly WordPressService wordPresService;

        public NewsController(WordPressService wordPresService)
        {
            this.wordPresService = wordPresService;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var model = new NewsViewModel()
            {
                News = await wordPresService.GetPostsByCategory((int)PostCategory.News)
            };

            return View(model);
        }

        //public object Rss()
        //{
        //    var rss = new RssModel();
        //    rss.channel.title = "Slamby Developers";
        //    rss.channel.link = "https://developers.slamby.com/news/rss";
        //    rss.channel.items.Add(new Item());
        //    return rss;
        //}
    }
}
