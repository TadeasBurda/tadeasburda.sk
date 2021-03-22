
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Models.TagHelpers
{
    public class PictureTagHelper : TagHelper
    {
        /// <summary>
        /// Example: .webp;.jpeg;...
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Example: xs:100px;sm:200px;md:300px;lg:400px;xl:500px;xxl:600px
        /// </summary>
        public string Media { get; set; }

        /// <summary>
        /// Example: 600w;500w;400w;300w;200w;...
        /// </summary>
        public string Sizes { get; set; }

        /// <summary>
        /// Src for original image. Must contain "original" whitout extension.
        /// Example: .../name-original
        /// </summary>
        public string Src { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync(); // save child(img)

            foreach (string format in Format.Split(";"))
                output.Content.AppendHtml(GetSource(format));

            output.Content.AppendHtml(content);
        }

        private string GetSizesAttribute()
        {
            List<string> sizes = new();
            foreach (string item in Media.Split(";"))
            {
                string[] media = item.Split(":");
                switch (media[0])
                {
                    case "xs":
                        sizes.Add($"(max-width: 576px) {media[1]}");
                        break;
                    case "sm":
                        sizes.Add($"(max-width: 767px) {media[1]}");
                        break;
                    case "md":
                        sizes.Add($"(max-width: 991px) {media[1]}");
                        break;
                    case "lg":
                        sizes.Add($"(max-width: 1199px) {media[1]}");
                        break;
                    case "xl":
                        sizes.Add($"(max-width: 1399px) {media[1]}");
                        break;
                    case "xxl":
                        sizes.Add($"(min-width: 1400px) {media[1]}");
                        break;
                }
            }
            return string.Join(",", sizes);
        }

        private TagBuilder GetSource(string format)
        {
            TagBuilder source = new("source");
            source.Attributes.Add("srcset", GetSrcsetAttribute(format));
            source.Attributes.Add("sizes", GetSizesAttribute());
            return source;
        }

        private string GetSrcsetAttribute(string format)
        {
            List<string> srcset = new();
            foreach (string width in Sizes.Split(";"))
                srcset.Add(Src.Replace("original", $"{width}{format} {width}"));
            return string.Join(",", srcset);
        }
    }
}
