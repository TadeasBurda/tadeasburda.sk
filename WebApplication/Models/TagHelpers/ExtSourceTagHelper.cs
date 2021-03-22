using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Models.TagHelpers
{
    public class ExtSourceTagHelper: TagHelper
    {
        /// <summary>
        /// .../example/name-original.jpeg
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 1000w;800w;500;...
        /// </summary>
        public string SourceWidth { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "source";

            List<string> srcset = new();
            foreach (string w in SourceWidth.Split(";"))
                srcset.Add($"{Source.Replace("original", $"{w}")} {w},");
            srcset.Add(Source);

            output.Attributes.SetAttribute("srcset", string.Join(" ", srcset));

            return base.ProcessAsync(context, output);
        }
    }
}
