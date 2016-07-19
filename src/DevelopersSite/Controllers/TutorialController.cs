using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DevelopersSite.Services;
using System.Linq;
using DevelopersSite.Models;
using DevelopersSite.Enums;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DevelopersSite.Controllers
{
    public class TutorialController : Controller
    {
        readonly WordPressService wordPressService;

        public TutorialController(WordPressService wordPressService)
        {
            this.wordPressService = wordPressService;
        }

        [Route("tutorial/{slug?}")]
        public async Task<IActionResult> Index(string slug = null)
        {
            var tutorials = (await wordPressService.GetPostsByCategory((int)PostCategory.Tutorials)).OrderByDescending(o => o.Id);
            var firstSlug = tutorials.Select(s => s.Slug).FirstOrDefault();

            if (string.IsNullOrEmpty(firstSlug))
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(slug))
            {    
                return RedirectToAction("Index", "Tutorial", new { slug = firstSlug });
            }

            var id = tutorials.Where(t => t.Slug == slug).Select(s => s.Id).Single();
            var model = new TutorialViewModel()
            {
                Tutorials = tutorials,
                Post = await wordPressService.GetPost(id)
            };

            ViewBag.Title = model.Post.Title.Rendered;

            return View(model);
        }
    }
}
