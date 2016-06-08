using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Slamby.SDK.Net.Managers;
using Slamby.SDK.Net;
using DevelopersSite.Services;
using DevelopersSite.Models;
using DevelopersSite.Enums;
using System.Globalization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DevelopersSite.Controllers
{
    public class HomeController : Controller
    {
        readonly WordPressService wordPressService;
        readonly SiteConfig siteConfig;

        public HomeController(WordPressService wordPressService, SiteConfig siteConfig)
        {
            this.siteConfig = siteConfig;
            this.wordPressService = wordPressService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new NewsViewModel()
            {
                News = await wordPressService.GetPostsByCategory((int)PostCategory.News)
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddSubscription(string name="", string company="", string email="")
        {
            try
            {
                var configuration = new Configuration
                {
                    ApiBaseEndpoint = new Uri(siteConfig.ApiBaseEndpoint),
                    ApiSecret = siteConfig.ApiSecret
                };

                var manager = new DocumentManager(configuration, "newsletter");
                var document = new
                {
                    id = Guid.NewGuid(),
                    name = name,
                    company = company,
                    email = email,
                    date = DateTime.Now.ToString(CultureInfo.InvariantCulture.DateTimeFormat.SortableDateTimePattern),
                    exported = false
                };

                var result = await manager.CreateDocumentAsync(document);
                if (result.IsSuccessFul)
                {
                    return Ok(new { success = false });
                }
            }
            catch
            {
                return HttpBadRequest(new { success = false, message = "Subscribe failed!" });
            }

            return HttpBadRequest(new { success = false, message = "Subscribe failed!" });
        }
    }
}
