﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net;
using DevelopersSite.Models;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DevelopersSite.Controllers
{
    public class HomeController : Controller
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
    }
}
