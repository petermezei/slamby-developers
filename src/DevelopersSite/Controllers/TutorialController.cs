using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Net;
using DevelopersSite.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DevelopersSite.Controllers
{
    public class TutorialController : Controller
    {
        [Route("tutorial/{id?}")]
        public IActionResult Index(int id=0)
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("http://13.79.162.110/wp-json/wp/v2/posts?categories=3");
                var tutorials = JsonConvert.DeserializeObject<List<WordpressPostModel>>(json);
                ViewBag.tutorials = tutorials;
                if (id==0)
                {
                    return RedirectToAction("Index", "Tutorial", new { id = tutorials[0].id });
                }
                var jsonPost = webClient.DownloadString(String.Format("http://13.79.162.110/wp-json/wp/v2/posts/{0}", id));
                var postContent = JsonConvert.DeserializeObject<WordpressPostModel>(jsonPost);
                ViewBag.post = postContent;
                return View();
            }
        }
    }
}
