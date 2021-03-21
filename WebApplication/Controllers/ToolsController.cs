using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.IO.Compression;
using WebApplication.Models;
using WebApplication.Models.Extensions;
using WebApplication.Models.Services;
using WebApplication.Models.ViewModels.ToolsController;

namespace WebApplication.Controllers
{
    public class ToolsController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly FileServices fileServices;
        private readonly ImageServices imageServices;
        private readonly ILogger<ToolsController> logger;
        public ToolsController(IWebHostEnvironment webHostEnvironment, ImageServices imageServices, FileServices fileServices, ILogger<ToolsController> logger)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.imageServices = imageServices;
            this.fileServices = fileServices;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult ImagesConvertor() => View(new ImagesConvertorViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ImagesConvertor(ImagesConvertorViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid(); // Create a unique code for a folder of images and ZIP file.
                string directoryPath = Path.Combine(webHostEnvironment.WebRootPath, "temporary-files", guid.ToString());

                try
                {
                    foreach (IFormFile image in model.UploadFiles)
                    {
                        // convert original and save
                        if (model.OutputWeb)
                            imageServices.ConvertImage(image, directoryPath, ImageServices.EImageFormat.WebP);
                        if (model.OutputJpeg)
                            imageServices.ConvertImage(image, directoryPath, ImageServices.EImageFormat.Jpeg);

                        // edit sizes and save
                        foreach (int width in model.Widths)
                        {
                            if (model.OutputWeb)
                                imageServices.ConvertImage(image, directoryPath, ImageServices.EImageFormat.WebP, width);
                            if (model.OutputJpeg)
                                imageServices.ConvertImage(image, directoryPath, ImageServices.EImageFormat.Jpeg, width);
                        }
                    }

                    ZipFile.CreateFromDirectory(directoryPath, $"{directoryPath}.zip"); // Create a ZIP from the folder in which the edited images.

                    FileStream fileStream = new($"{directoryPath}.zip", FileMode.Open);
                    return File(fileStream, "application/zip", "kovertované-obrázky.zip");
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

    }
}
