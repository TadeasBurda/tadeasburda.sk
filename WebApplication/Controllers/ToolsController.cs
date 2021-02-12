using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.IO.Compression;
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
                fileServices.SaveFileToDirectoryAsync(model.UploadFile, "wwwroot/temporary-files");
            else
                model.UploadFile = null; // The file is deleted to display the default image in View.

            return View(model);
        }
        #endregion

        #region api
        [HttpPost, Route("/api/ConvertImgToWebP")]
        public IActionResult ConvertImgToWebP(ConvertImgToWebPViewModel model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            try
            {
                Guid guid = Guid.NewGuid(); // Create a unique code for a folder of images and ZIP file.

                // Convert and resize the image.
                foreach (int width in model.Widths)
                    imageServices.ConverFileToWebP(new FileInfo($"wwwroot/temporary-files/{model.FileName}"), width, $"wwwroot/temporary-files/{guid}");

                ZipFile.CreateFromDirectory($"wwwroot/temporary-files/{guid}", $"wwwroot/temporary-files/{guid}.zip"); // Create a ZIP from the folder in which the edited images.

                return Content(Url.Content($"~/temporary-files/{guid}.zip")); // Send the path to the ZIP file.
            }
            catch(Exception e)
            {
                logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }
}
