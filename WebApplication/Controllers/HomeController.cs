using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleMvcSitemap;
using SimpleMvcSitemap.Images;
using System.Collections.Generic;
using System.Diagnostics;
using WebApplication.Models;
using WebApplication.Services;

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
        public IActionResult Index()
        {
            return View();
        }

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
        #endregion

        #region Helpers
        private List<SitemapNode> GetNodes()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();
            nodes.AddRange(GetViewsNodes());
            nodes.Add(new SitemapNode(HttpContext.Request.PathBase) { Images = GetImagesNodes() });

            return nodes;
        }
        private List<SitemapNode> GetViewsNodes()
        {
            List<SitemapNode> list = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index", "Home")),
                new SitemapNode(Url.Action("Privacy", "Home")),
            };

            return list;
        }
        private List<SitemapImage> GetImagesNodes()
        {
            List<SitemapImage> list = new List<SitemapImage>
            {
                new SitemapImage("/favicon.ico"),
                new SitemapImage("/share-img.webp")
            };

            foreach (string url in fileServices.ReturnFilePaths("wwwroot/img"))
                list.Add(new SitemapImage(url));

            return list;
        }
        #endregion
    }
}
