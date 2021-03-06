using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace WebApplication.Models.ValidationAttributes
{
    public class AllowedExtensionsAttribute: ValidationAttribute
    {
        private readonly string[] extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            this.extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
                if (!extensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                    return new ValidationResult($"Formát súboru {Path.GetExtension(file.FileName).ToUpper()} nie je podporovaný.");

            return ValidationResult.Success;
        }
    }
}
