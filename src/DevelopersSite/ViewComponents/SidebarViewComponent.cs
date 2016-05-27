using DevelopersSite.Models;
using Microsoft.AspNet.Mvc;

namespace DevelopersSite.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(DocumentViewModel model)
        {
            return View(model);
        }
    }
}
