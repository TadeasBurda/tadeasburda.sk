using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.ToolsViewModels;

namespace WebApplication.Controllers
{
    public class ToolsController : Controller
    {
        #region Views()
        [HttpGet]
        public IActionResult ImagesConvertor()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ImagesConvertor(ImagesConvertorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return RedirectToAction(nameof(ImagesConvertor));
        }
        #endregion
    }
}
