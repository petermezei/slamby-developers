using DevelopersSite.Models;
using DevelopersSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevelopersSite.Controllers
{
    public class DocsController : Controller
    {
        readonly DocumentService documentService;

        public DocsController(DocumentService documentService)
        {
            this.documentService = documentService;
        }

        [Route("scan")]
        public IActionResult Scan()
        {
            documentService.ScanDocuments();
            return RedirectToAction("Index");
        }

        public IActionResult Home()
        {
            return RedirectToAction("Index", "Docs", documentService.GetRouteParams("API"));
        }

        [Route("docs/{product?}/{version?}/{file?}")]
        public IActionResult Index(string product, string version, string file = "index")
        {
            var document = documentService.GetProduct(product);
            if (document == null)
            {
                return RedirectToAction("Index", "Docs", documentService.GetRouteParams("API"));
            }
            if (string.IsNullOrEmpty(version))
            {
                return RedirectToAction("Index", "Docs", documentService.GetRouteParams(product));
            }

            var docVersion = documentService.GetVersion(document, version);

            var currentUrl = Url.Action("Index", "Docs", new { product = product, version = docVersion.Version });
            var filename = documentService.ValidateFilename(docVersion, file);

            var model = new DocumentViewModel
            {
                Document = document,
                Version = docVersion.Version,
                Content = documentService.GetContent(docVersion, filename),
                CurrentUrl = currentUrl,
                IsStartDocument = string.Equals(filename, docVersion.StartFilename),
                DocumentCount = docVersion.ContentFilenames.Count
            };

            if (model.IsStartDocument &&  
                !string.IsNullOrEmpty(docVersion.TocFilename))
            {
                model.TocContent = documentService.GetContent(docVersion, docVersion.TocFilename);
            }

            ViewBag.StaticBase = docVersion.StaticBase;
            ViewBag.CurrentProduct = product;
            ViewBag.Title = model.Document.Product;
            ViewBag.IsNavbarTopFixed = true;

            return View(model);
        }
    }
}