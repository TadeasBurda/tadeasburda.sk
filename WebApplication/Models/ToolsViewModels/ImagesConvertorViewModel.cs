using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.ToolsViewModels
{
    public class ImagesConvertorViewModel
    {
        [Display(Name = "Obrázok")]
        [Required(ErrorMessage = "Musíš nahrať obrázok.")]
        public IFormFile Image { get; set; }

        [Display(Name = "Šírka/y")]
        [Required(ErrorMessage = "Musíš zadať šírku/y.")]
        public string Sizes { get; set; }
    }
}
