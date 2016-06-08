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

        [Route("tutorial/{id?}")]
        public async Task<IActionResult> Index(int id = 0)
        {
            var tutorials = await wordPressService.GetPostsByCategory((int)PostCategory.Tutorials);
            var firstId = tutorials.Select(s => s.Id).FirstOrDefault();

            if (firstId == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == 0)
            {    
                return RedirectToAction("Index", "Tutorial", new { id = firstId });
            }

            var model = new TutorialViewModel()
            {
                Tutorials = tutorials,
                Post = await wordPressService.GetPost(id)
            };

            return View(model);
        }
    }
}
