using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WebApplication.Models.Services
{
    public class ImageServices
    {
        public enum EImageFormat { Jpeg, WebP }

        /// <summary>
        /// Converts the image to a format and saves it to a folder.
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="saveDirectory"></param>
        /// <param name="saveFormat"></param>
        /// <param name="width">If the width is null, the original is saved in the desired size.</param>
        public void ConvertImage(IFormFile formFile, string saveDirectory, EImageFormat saveFormat, int? width = null)
        {
            Image image = Image.FromStream(formFile.OpenReadStream());
            image = EditImageSizes(image); // edit image weidth/ height

            // create folder if not exist
            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);

            string savePath = Path.Combine(saveDirectory, GetNewName(formFile, saveFormat, width)); // create new name

            if (saveFormat == EImageFormat.Jpeg) // save as Jpeg
                image.Save(savePath, ImageFormat.Jpeg);
            else if (saveFormat == EImageFormat.WebP) // save as WebP
            {
                using ImageFactory imageFactory = new(preserveExifData: true);
                imageFactory.Load(image)
                                .Format(new WebPFormat { Quality = 90 })
                                    .Save(savePath);
            }
        }

        /// <summary>
        /// If width is specified, adjusts the width / height of the image to maintain the aspect ratio.
        /// If no width is specified, it returns without change.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private static Image EditImageSizes(Image image, int? width = null)
        {
            if (!width.HasValue) // return original
                return image;

            // ***** calculate new height *****

            if (width.Value > image.Width)
                throw new Exception($"The specified width cannot be greater than the original image width.[NewWidth = {width.Value} & OldWidth = {image.Width}]");

            int height = (int)Math.Round((image.Height / (image.Width / (double)width.Value)));

            return new Bitmap(image, width.Value, height);
        }

        /// <summary>
        /// Edit image name by format.
        /// If a width is specified, it is added to the name.
        /// If no width is specified, it is added to the -original name.
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="imageFormat"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private static string GetNewName(IFormFile formFile, EImageFormat imageFormat, int? width = null)
        {
            if (imageFormat == EImageFormat.Jpeg)
            {
                if (width.HasValue)
                    return $"{Path.GetFileNameWithoutExtension(formFile.FileName)}-w{width.Value}.jpeg";
                else
                    return $"{Path.GetFileNameWithoutExtension(formFile.FileName)}-original.jpeg";
            }
            else if (imageFormat == EImageFormat.WebP)
            {
                if (width.HasValue)
                    return $"{Path.GetFileNameWithoutExtension(formFile.FileName)}-w{width.Value}.webp";
                else
                    return $"{Path.GetFileNameWithoutExtension(formFile.FileName)}-original.webp";
            }

            throw new ArgumentNullException(nameof(imageFormat));
        }
    }
}
