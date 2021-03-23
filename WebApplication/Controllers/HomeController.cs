using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleMvcSitemap;
using SimpleMvcSitemap.Images;
using System.Collections.Generic;
using System.Diagnostics;
using WebApplication.Models;
using WebApplication.Models.Extensions;
using WebApplication.Models.Services;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly FileServices fileServices;
        private readonly ILogger<HomeController> logger;
        public HomeController(ILogger<HomeController> logger, FileServices fileServices)
        {
            this.logger = logger;
            this.fileServices = fileServices;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/api/Error/{statusCode:int}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            return statusCode switch
            {
                404 => RedirectToAction("Index", "Home", new { flashMessage = "Odkaz na ktorý ste klikli nikam nevedie, ale viedol na túto stránku... Možné príčiny: odkaz je starý, chybný, zablokovaný, atď." }),
                _ => RedirectToAction("Index", "Home", new { flashMessage = "Niečo sa pokazilo... Informáciu o chybe som zaregistroval a budem ju ďalej spracovať. Ďakujem za pochopenie." }),
            };
        }

        [Route("/sitemap.xml"), HttpGet]
        public ActionResult GetSitemap() => new SitemapProvider().CreateSitemap(new SitemapModel(GetNodes()));

        [ResponseCache(Duration = 180, Location = ResponseCacheLocation.Any)]
        public IActionResult Index(string flashMessage = null)
        {
            if (!string.IsNullOrEmpty(flashMessage))
                this.AddFlashMessage(flashMessage, FlashMessageType.Info);

            return View();
        }

        [ResponseCache(Duration = 180, Location = ResponseCacheLocation.Any)]
        public IActionResult Portfolio() => View();

        [ResponseCache(Duration = 180, Location = ResponseCacheLocation.Any)]
        public IActionResult Privacy()
        {
            return View();
        }

        private List<SitemapImage> GetImagesNodes()
        {
            List<SitemapImage> list = new();

            foreach (string url in fileServices.ReturnFilePaths("wwwroot/img"))
                list.Add(new SitemapImage(url));

            return list;
        }

        private List<SitemapNode> GetNodes()
        {
            List<SitemapNode> nodes = new();
            nodes.AddRange(GetViewsNodes());
            nodes.Add(new SitemapNode(HttpContext.Request.PathBase) { Images = GetImagesNodes() });

            return nodes;
        }
        private List<SitemapNode> GetViewsNodes()
        {
            List<SitemapNode> list = new()
            {
                new SitemapNode(Url.Action("Index", "Home")),
                new SitemapNode(Url.Action("Privacy", "Home")),
                new SitemapNode(Url.Action("ImagesConvertor", "Tools"))
            };

            return list;
        }
    }
}
