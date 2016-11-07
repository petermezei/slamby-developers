using System.Threading.Tasks;
using DevelopersSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevelopersSite.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public SidebarViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(DocumentViewModel model)
        {
            if (model == null)
            {
                return Content("");
            }

            return View(model);
        }
    }
}
