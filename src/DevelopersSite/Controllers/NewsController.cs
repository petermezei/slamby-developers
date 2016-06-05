using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net;
using Newtonsoft.Json;
using DevelopersSite.Models;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DevelopersSite.Controllers
{
    public class NewsController : Controller
    {
        // GET: /<controller>/
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
