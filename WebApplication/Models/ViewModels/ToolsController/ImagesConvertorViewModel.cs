using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WebApplication.ValidationAttributes;

namespace WebApplication.Models.ViewModels.ToolsController
{
    public class ImagesConvertorViewModel
    {
        [Required(ErrorMessage = "Pole je povinné.")]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png", ".webp" })]
        public IFormFile UploadFile { get; set; }

        public string FileName { get; set; }
    }
}
