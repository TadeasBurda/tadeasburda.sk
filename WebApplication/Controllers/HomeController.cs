using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleMvcSitemap;
using SimpleMvcSitemap.Images;
using System.Collections.Generic;
using System.Diagnostics;
using WebApplication.Models;
using WebApplication.Services;
using WebApplication.Extensions;
using WebApplication.Classes;
using System;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        #region private
        private readonly ILogger<HomeController> logger;
        #endregion

        #region Services
        private readonly IFileServices fileServices;
        #endregion

        #region Constructs
        public HomeController(ILogger<HomeController> logger, IFileServices fileServices)
        {
            this.logger = logger;
            this.fileServices = fileServices;
        }
        #endregion

        #region Views()
        [ResponseCache(Duration = 180, Location = ResponseCacheLocation.Any)]
        public IActionResult Index() => View();

        [ResponseCache(Duration = 180, Location = ResponseCacheLocation.Any)]
        public IActionResult Portfolio() => View();

        [ResponseCache(Duration = 180, Location = ResponseCacheLocation.Any)]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region api
        [Route("/sitemap.xml"), HttpGet]
        public ActionResult GetSitemap() => new SitemapProvider().CreateSitemap(new SitemapModel(GetNodes()));

        [Route("/api/Error")]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                logger.LogError($"StatusCode: {statusCode.Value}");

                switch (statusCode.Value)
                {
                    case 404:
                        this.AddFlashMessage("Odkaz na ktorý ste klikli nikam nevedie, ale viedol na túto stránku... Možné príčiny: odkaz je starý, chybný, zablokovaný, atď. ", FlashMessageType.Info);
                        break;
                    default:
                        this.AddFlashMessage("Niečo sa pokazilo... Informáciu o chybe som zaregistroval a budem ju ďalej spracovať. Ďakujem za pochopenie ", FlashMessageType.Danger);
                        break;
                }
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helpers
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
        private List<SitemapImage> GetImagesNodes()
        {
            List<SitemapImage> list = new();

            foreach (string url in fileServices.ReturnFilePaths("wwwroot/img"))
                list.Add(new SitemapImage(url));

            return list;
        }
        #endregion
    }
}
