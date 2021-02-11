using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.IO.Compression;
using WebApplication.Models.ViewModels.ToolsController;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class ToolsController : Controller
    {
        #region Services
        private readonly IImageServices imageServices;
        private readonly IFileServices fileServices;
        #endregion

        public ToolsController(IImageServices imageServices, IFileServices fileServices)
        {
            this.imageServices = imageServices;
            this.fileServices = fileServices;
        }

        #region Views()
        [HttpGet]
        public IActionResult ImagesConvertor() => View(new ImagesConvertorViewModel());
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ImagesConvertor(ImagesConvertorViewModel model)
        {
            if (ModelState.IsValid)
                fileServices.SaveFileToDirectoryAsync(model.UploadFile, "wwwroot/img/img-convertor");
            else
                model.UploadFile = null; // The file is deleted to display the default image in View.

            return View(model);
        }
        #endregion

        #region api
        [HttpGet, Route("/api/ConvertImgToWebP")]
        public IActionResult ConvertImgToWebP(ConvertImgToWebPViewModel model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("wwwroot/img/img-convertor/");
              
                foreach (int width in model.Widths)
                    imageServices.ConverFileToWebP(new FileInfo($"wwwroot/img/img-convertor/{model.FileName}"), width, directoryInfo);

                ZipFile.CreateFromDirectory("wwwroot/img/img-convertor/", "wwwroot/temporary-files/new.zip");

                return File("~/new.zip", "application/zip", "new.zip");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }
}
