#pragma checksum "C:\Users\tadea\source\repos\TadeasBurda\tadeasburda.sk\WebApplication\Views\Tools\ImagesConvertor\_DescriptionPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "12c633705ea52fd2f45c99b01b037707c1b95689"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Tools_ImagesConvertor__DescriptionPartial), @"mvc.1.0.view", @"/Views/Tools/ImagesConvertor/_DescriptionPartial.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\tadea\source\repos\TadeasBurda\tadeasburda.sk\WebApplication\Views\_ViewImports.cshtml"
using WebApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\tadea\source\repos\TadeasBurda\tadeasburda.sk\WebApplication\Views\_ViewImports.cshtml"
using WebApplication.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\tadea\source\repos\TadeasBurda\tadeasburda.sk\WebApplication\Views\_ViewImports.cshtml"
using WebApplication.Models.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\tadea\source\repos\TadeasBurda\tadeasburda.sk\WebApplication\Views\_ViewImports.cshtml"
using WebApplication.Models.ViewModels.ToolsController;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"12c633705ea52fd2f45c99b01b037707c1b95689", @"/Views/Tools/ImagesConvertor/_DescriptionPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"14252da3dc268f573a11d493754f2f681ff98eb0", @"/Views/_ViewImports.cshtml")]
    public class Views_Tools_ImagesConvertor__DescriptionPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<section>\r\n    <h1># Konvertor obrázkov</h1>\r\n\r\n");
            WriteLiteral(@"    <div class=""alert alert-info"" role=""alert"">
        Kód je možné si preštudovať na mojom GitHube(<a class=""text-decoration-none"" href=""https://github.com/TadeasBurda/tadeasburda.sk/blob/master/WebApplication/Controllers/ToolsController.cs"" target=""_blank"">kód kontroleru</a>).
    </div>

");
            WriteLiteral(@"    <article>
        <h2>1. Popis</h2>
        <p>Potrebuješ konvertovať tvoje obrázky do nového formátu od Google, ktorý sa volá <a class=""text-decoration-none"" href=""https://developers.google.com/speed/webp"" target=""_blank"">WebP</a>? Tento nástroj ti to umožní! Ak nevieš čo je formát WebP, tak v skratke sa jedná o <strong>moderný formát</strong> obrázkov, ktorý bol vytvorený <strong>pre weby</strong>(jeho použitie je ale možné kdekoľvek). Tento formát sa vyznačuje <strong>nízkou dátovou veľkosťou</strong>, <strong>veľkou kvalitou kompresie</strong> a v poslednej dobe aj širokým rozšírením na weboch(<a class=""text-decoration-none"" href=""https://www.cnet.com/news/facebook-tries-googles-webp-image-format-users-squawk/"" target=""_blank"">implementoval ho už aj Facebook</a>, aby ušetril miesto na serveroch). Tvoj web z použitím WebP, bude rýchlejší a bude držať krok z dobou!</p>
        <p>Prečo sa oplatí mať na webe obrázky v rôznej veľkosti? Použitím HTML tagu <a class=""text-decoration-none"" href=""https://ww");
            WriteLiteral(@"w.w3schools.com/tags/tag_picture.asp"" target=""_blank"">picture</a> alebo <a class=""text-decoration-none"" href=""https://developer.mozilla.org/en-US/docs/Learn/HTML/Multimedia_and_embedding/Responsive_images"" target=""_blank"">resposive img</a> , je veľmi výhodné v dnešnej dobe kde väčšina užívateľov navštívi tvoj web z mobilných zariadení. Každý užívateľ ocení že mu <strong>šetríš dáta</strong> tým že mu do prehliadača odošleš len tak veľký obrázok ako potrebuje. Načítať obrázok 1920x1080px na telefóne, ktorý má šírku „len“ 550px je trochu od veci... Ak pri stavbe tvojho webu, alebo pri facelite staršieho webu budeš myslieť na <strong>úsporu dát</strong>, <strong>rýchlosť webu</strong> a <strong>responzivitu</strong>, určite tým získaš iba body++ 🙂</p>
        <br />
        <h2>2. Návod</h2>
        <p>Vyber obrázky ktoré chceš konvertovať(aktuálna podpora pre formáty JPG a PNG, <i>ku dňu 20.2.2021</i>) a do tabuľky zadaj rozmery ktoré chceš aby sa ti vrátili – zadaj len šírku, výška sa dopočíta automaticky ");
            WriteLiteral("aby sa zachoval pomer strán obrázku. Po konvertovaní sa ti automaticky stiahne ZIP súbor z obrázkami.</p>\r\n    </article>\r\n\r\n");
            WriteLiteral("    <div class=\"alert alert-info\" role=\"alert\">\r\n        Ako to funguje sa možeš dočítať v <a class=\"text-decoration-none\" href=\"#\" target=\"_blank\">článku</a> na mojom blogu.\r\n    </div>\r\n</section>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
