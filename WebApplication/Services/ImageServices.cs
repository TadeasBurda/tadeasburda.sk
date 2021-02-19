using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.IO;

namespace WebApplication.Services
{
    public interface IImageServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="newWidth">New width - height is calculated.</param>
        /// <param name="directoryPath">The folder in which to save the new file.</param>
        void ConverFileToWebP(IFormFile image, int newWidth, string directoryPath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="directoryPath">The folder in which to save the new file.</param>
        void ConverFileToWebP(IFormFile image, string directoryPath);
    }

    public class ImageServices: IImageServices
    {
        #region private Methods()
        /// <summary>
        /// Recalculate the height of the image to maintain the aspect ratio.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="newWidth"></param>
        /// <returns></returns>
        private static Size CalculateHeight(IFormFile file, int newWidth)
        {
            using Image image = Image.FromStream(file.OpenReadStream());

            if (newWidth > image.Width)
                throw new Exception($"The specified width cannot be greater than the original image width.[NewWidth = {newWidth} & OldWidth = {image.Width}]");

            int height = (int)Math.Round((image.Height / (image.Width / (double)newWidth)));

            return new Size(newWidth, height);
        }
        #endregion

        #region public Methods()
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="newWidth">New width - height is calculated.</param>
        /// <param name="directory">The folder in which to save the new file.</param>
        public void ConverFileToWebP(IFormFile image, int newWidth, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string newName = image.ContentType switch
            {
                "image/jpg" => image.FileName.Replace(".jpg", $"-{newWidth}w.webp"),
                "image/jpeg" => image.FileName.Replace(".jpg", $"-{newWidth}w.webp"),
                "image/png" => image.FileName.Replace(".png", $"-{newWidth}w.webp"),
                _ => throw new Exception($"Image format {image.ContentType} is not supported."),
            };

            using ImageFactory imageFactory = new ImageFactory(preserveExifData: true);
            imageFactory.Load(image.OpenReadStream())
                        .Resize(CalculateHeight(image, newWidth))
                        .Format(new WebPFormat { Quality = 90 })
                        .Save(Path.Combine(directoryPath, newName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="directory">The folder in which to save the new file.</param>
        public void ConverFileToWebP(IFormFile image, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string newName = image.ContentType switch
            {
                "image/jpg" => image.FileName.Replace(".jpg", "-original.webp"),
                "image/jpeg" => image.FileName.Replace(".jpg", "-original.webp"),
                "image/png" => image.FileName.Replace(".png", "-original.webp"),
                _ => throw new Exception($"Image format {image.ContentType} is not supported."),
            };

            using ImageFactory imageFactory = new ImageFactory(preserveExifData: true);
            imageFactory.Load(image.OpenReadStream())
                        .Format(new WebPFormat { Quality = 90 })
                        .Save(Path.Combine(directoryPath, newName));
        }
        #endregion
    }
}
