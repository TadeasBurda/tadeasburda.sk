using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.IO.Compression;
using WebApplication.Classes;
using WebApplication.Extensions;
using WebApplication.Models.ViewModels.ToolsController;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class ToolsController : Controller
    {
        #region private
        private readonly ILogger<ToolsController> logger;
        #endregion

        #region Services
        private readonly IImageServices imageServices;
        private readonly IFileServices fileServices;
        #endregion

        public ToolsController(IImageServices imageServices, IFileServices fileServices, ILogger<ToolsController> logger)
        {
            this.imageServices = imageServices;
            this.fileServices = fileServices;
            this.logger = logger;
        }

        #region Views()
        [HttpGet]
        public IActionResult ImagesConvertor() => View(new ImagesConvertorViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ImagesConvertor(ImagesConvertorViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid(); // Create a unique code for a folder of images and ZIP file.

                try
                {
                    foreach (IFormFile image in model.UploadFiles)
                    {
                        imageServices.ConverFileToWebP(image, $"wwwroot/temporary-files/{guid}"); // Convert original to WebP.

                        foreach (int width in model.Widths)
                            imageServices.ConverFileToWebP(image, width, $"wwwroot/temporary-files/{guid}"); // Convert the original to WebP and change its sizes scale.
                    }

                    ZipFile.CreateFromDirectory($"wwwroot/temporary-files/{guid}", $"wwwroot/temporary-files/{guid}.zip"); // Create a ZIP from the folder in which the edited images.

                    return File($"temporary-files/{guid}.zip", "application/zip", "kovertované-obrázky.zip");
                }
                catch (Exception e)
                {
                    logger.LogError(e, e.Message);
                    this.AddFlashMessage("Pri konvertovaní obrázku/ov došlo k chybe. Skúste to ešte raz...", FlashMessageType.Danger);

                    return RedirectToAction(nameof(ImagesConvertor));
                }
            }

            return View(model);
        }
        #endregion
    }
}
