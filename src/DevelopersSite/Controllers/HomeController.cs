using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net;
using DevelopersSite.Models;
using Newtonsoft.Json;
using Slamby.SDK.Net.Managers;
using Slamby.SDK.Net;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DevelopersSite.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("http://13.79.162.110/wp-json/wp/v2/posts?categories=2");
                var news = JsonConvert.DeserializeObject<List<WordpressPostModel>>(json);
                ViewBag.news = news;
                return View();
            }
        }
        [HttpPost]
        public async Task AddSubscription(string name="", string company="", string email="")
        {
            try
            {
                var configuration = new Configuration
                {
                    ApiBaseEndpoint = new Uri("https://europe.slamby.com/publi24/"),
                    ApiSecret = "publi249876"
                };

                var manager = new DocumentManager(configuration, "newsletter");
                Random rnd = new Random();
                var document = new
                {
                    id = rnd.Next(1, 1000000),
                    name = name,
                    company = company,
                    email = email,
                    date = "2016-06-05"
                };

                var result = await manager.CreateDocumentAsync(document);
            }
            catch
            {
            }
        }
    }
}
