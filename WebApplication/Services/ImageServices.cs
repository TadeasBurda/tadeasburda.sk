using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
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
        /// <param name="fileInfo"></param>
        /// <param name="newWidth">New width - height is calculated.</param>
        /// <param name="directory">The folder in which to save the new file.</param>
        void ConverFileToWebP(FileInfo fileInfo, int newWidth, string directoryPath);
    }

    public class ImageServices: IImageServices
    {
        #region private Methods()
        /// <summary>
        /// Recalculate the height of the image to maintain the aspect ratio.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="newWidth"></param>
        /// <returns></returns>
        private static Size CalculateHeight(Image image, int newWidth)
        {
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
        public void ConverFileToWebP(FileInfo fileInfo, int newWidth, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            using ImageFactory imageFactory = new ImageFactory(preserveExifData: true);
            imageFactory.Load(fileInfo.FullName)
                        .Resize(CalculateHeight(Image.FromFile(fileInfo.FullName), newWidth))
                        .Format(new WebPFormat { Quality = 90 })
                        .Save(Path.Combine(directoryPath, fileInfo.Name.Replace(fileInfo.Extension, $"-{newWidth}w.webp")));
        }
        #endregion
    }
}
