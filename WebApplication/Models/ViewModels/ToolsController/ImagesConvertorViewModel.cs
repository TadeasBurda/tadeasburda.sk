using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication.ValidationAttributes;

namespace WebApplication.Models.ViewModels.ToolsController
{
    public class ImagesConvertorViewModel
    {
        [Required(ErrorMessage = "Musíš nahrať aspoň jeden obrázok.")]
        [Display(Name = "Obrázok/y")]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png", ".webp" })]
        public List<IFormFile> UploadFiles { get; set; }

        [Required(ErrorMessage = "Musíš zadať hodnotu.")]
        [Display(Name = "Šírka/y")]
        [MinLength(1)]
        public List<int> Widths { get; set; }

        public ImagesConvertorViewModel()
        {
            UploadFiles = new List<IFormFile>();
            Widths = new List<int>() 
            {
                1
            };
        }
    }
}
