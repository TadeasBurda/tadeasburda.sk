using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Models.TagHelpers
{
    public class ExtImgTagHelper : TagHelper
    {
        /// <summary>
        /// Must contain: original
        /// Example: .../example/name-original.jpeg
        /// </summary>
        public string Src { get; set; }

        /// <summary>
        /// Example: 999w;500w;321w;...
        /// </summary>
        public string SourceWidth { get; set; }

        /// <summary>
        /// (max-width: 576px)
        /// </summary>
        public int XSmall { get; set; }

        /// <summary>
        /// (min-width: 576px)
        /// </summary>
        public int Small { get; set; }

        /// <summary>
        /// (min-width: 768px)
        /// </summary>
        public int Medium { get; set; }

        /// <summary>
        /// (min-width: 992px)
        /// </summary>
        public int Lagre { get; set; }

        /// <summary>
        /// (min-width: 1200px)
        /// </summary>
        public int ExtraLarge { get; set; }

        /// <summary>
        /// (max-width: 1400px)
        /// </summary>
        public int ExtraExtraLarge { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";

            // create srcset
            List<string> srcset = new();
            foreach (string width in SourceWidth.Split(";"))
                srcset.Add($"{Src.Replace("original", $"{width}")} {width}");
            output.Attributes.SetAttribute("srcset", string.Join(",", srcset));

            // create sizes
            List<string> sizes = new()
            {
                $"(max-width: 576px) {XSmall}px",
                $"(min-width: 576px) {Small}px",
                $"(min-width: 768px) {Medium}px",
                $"(min-width: 992px) {Lagre}px",
                $"(min-width: 1200px) {ExtraLarge}px",
                $"(max-width: 1400px) {ExtraExtraLarge}px"
            };
            output.Attributes.SetAttribute("sizes", string.Join(",", sizes));

            return base.ProcessAsync(context, output);
        }
    }
}
