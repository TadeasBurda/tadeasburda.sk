using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Models.TagHelpers
{
    public class ExtImgTagHelper: TagHelper
    {
        /// <summary>
        /// .../example/name-original.jpeg
        /// </summary>
        public string Src { get; set; }

        /// <summary>
        /// 1000w;800w;500;...
        /// </summary>
        public string SourceWidth { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";

            List<string> srcset = new();
            foreach (string w in SourceWidth.Split(";"))
                srcset.Add($"{Src.Replace("original", $"{w}")} {w},");

            string s = string.Join(" ", srcset);
            output.Attributes.SetAttribute("srcset", s.Remove(s.Length - 1));

            return base.ProcessAsync(context, output);
        }
    }
}
