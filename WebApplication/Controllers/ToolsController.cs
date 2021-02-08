using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;
using WebApplication.Extensions;
using WebApplication.Classes;
using System.IO;

namespace WebApplication.Controllers
{
    public class ToolsController : Controller
    {
        #region Services
        private readonly IImageServices imageServices;
        #endregion

        public ToolsController(IImageServices imageServices)
        {
            this.imageServices = imageServices;
        }

        #region Views()
        [HttpGet]
        public IActionResult ImagesConvertor() => View();
        #endregion

        #region api
        [HttpGet, Route("/api/ConvertImgToWebP")]
        public IActionResult ConvertImgToWebP(IFormFile formFile, int width)
        {
            if(formFile == null || formFile.Length == 0)
            {
                this.AddFlashMessage("Nenahral si súbor pre konvertovanie.", FlashMessageType.Danger);
                return Redirect(nameof(ImagesConvertor));
            }

            MemoryStream fileStream = imageServices.Convert(formFile, width);
            string fileDownloadName = $"{formFile.FileName}-{width}w.webp";

            return File(fileStream, "image/webp", fileDownloadName);
        }
        #endregion
    }
}
