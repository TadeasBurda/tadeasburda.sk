﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication.Models.ViewModels.ToolsController
{
    public class ConvertImgToWebPViewModel
    {
        [Required]
        [JsonPropertyName("fileName")]
        public string FileName { get; set; }

        [Required]
        [JsonPropertyName("widths")]
        [MinLength(1)]
        public int[] Widths { get; set; }
    }
}