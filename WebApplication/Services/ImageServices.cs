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
        /// Convert image to WebP and adjust its dimensions.
        /// Note: The width is calculated according to the height to maintain the aspect ratio.
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        MemoryStream Convert(IFormFile formFile, int width);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="newWidth">New width - height is calculated.</param>
        /// <param name="directory">The folder in which to save the new file.</param>
        void ConverFileToWebP(FileInfo fileInfo, int newWidth, DirectoryInfo directory);
    }

    public class ImageServices: IImageServices
    {
        #region private Methods()
        private Size CalculateHeight(Image image, int width)
        {
            if (width < image.Width)
                throw new Exception("Zadaná výška nesmie byť menšia než pôvodná výška obrázku.");

            int height = (int)Math.Round((image.Height / (image.Width / (double)width)));
            return new Size(width, height);
        }
        #endregion

        #region public Methods()
        public MemoryStream Convert(IFormFile formFile, int width)
        {
            using MemoryStream inputStream = new MemoryStream();
            formFile.CopyTo(inputStream);

            using MemoryStream outputStream = new MemoryStream();

            using ImageFactory imageFactory = new ImageFactory(preserveExifData: true);
            imageFactory.Load(inputStream)
                        .Resize(CalculateHeight(Image.FromStream(inputStream), width))
                        .Format(new WebPFormat { Quality = 90 })
                        .Save(outputStream);

            return outputStream;
        }

        public void ConverFileToWebP(FileInfo fileInfo, int newWidth, DirectoryInfo directory)
        {
            using ImageFactory imageFactory = new ImageFactory(preserveExifData: true);
            imageFactory.Load(fileInfo.FullName)
                        .Resize(CalculateHeight(Image.FromFile(fileInfo.FullName), newWidth))
                        .Format(new WebPFormat { Quality = 90 })
                        .Save(directory.FullName);
        }
        #endregion
    }
}
